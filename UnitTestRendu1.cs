using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Reflection.Metadata;

namespace PSI_Rendu_1
{
    [TestClass]
    public class UnitTestRendu1
    {
        [TestMethod]
        /// Teste la methode AjouterLien (s'il y a bien 2 liens et si le lien entre 1 et 2 a �t� cr�� correctement)
        public void TestMethodLien()
        {
            Graphe graphe = new Graphe();
            graphe.Noeuds.Clear();
            graphe.Lien.Clear();

            graphe.Noeuds.Add(new Noeud(1));
            graphe.Noeuds.Add(new Noeud(2));

            graphe.AjouterLienPublic(1,2);

            bool lienSourceCible = false;
            bool lienCibleSource = false;

            foreach (Lien lien in graphe.Lien)
            {
                if (lien.Source.Id == 1 && lien.Cible.Id == 2)
                {
                    lienSourceCible = true;
                }
                if (lien.Source.Id == 2 && lien.Cible.Id == 1)
                {
                    lienCibleSource = true;
                }
            }

            Assert.IsTrue(lienSourceCible);
            Assert.IsTrue(lienCibleSource);

            Assert.AreEqual(2, graphe.Lien.Count);
        }

        [TestMethod]
        /// teste la methode ConstructionMatriceAdj (sur les 4 premiers noeuds ici)
        /// la methode est appel�e directement dans le constructeur 

        public void TestMethodMatAdj()
        {
            Graphe graphe = new Graphe();

            int[,] matadjtest = { { 0, 1, 1, 1 }, { 1, 0, 1, 1 }, { 1, 1, 0, 1 }, { 1, 1, 1, 0 } };

            for (int i = 0; i < matadjtest.GetLength(0); i++)
            {

                for (int j = 0; j < matadjtest.GetLength(0); j++)
                {
                    Assert.AreEqual(matadjtest[i, j], graphe.MatriceAdjacence[i, j]);

                }
            }

        }

        [TestMethod]
        /// teste la methode ConstructionListeAdj (les 3 premiers noeuds) 
        /// la methode est appel�e directement dans le constructeur 
        public void TestMethodListAdj()
        {
            Graphe graphe = new Graphe();

            List<List<int>> listeAdjacenceTest = new List<List<int>> {
                new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 11, 12, 13, 14, 18, 20, 22, 32 },
                new List<int> { 1, 3, 4, 8, 14, 18, 20, 22, 31 },
                new List<int> { 1, 2, 4, 8, 9, 10, 14, 28, 29, 33 }
                };
            for (int i = 0; i < listeAdjacenceTest.Count; i++)
            {
                CollectionAssert.AreEqual(listeAdjacenceTest[i], graphe.ListeAdjacence[i]);
            }
        }

        [TestMethod]
        /// teste la methode DFS avec un nouveau graphe (en convertissant la sortie console en string)
        public void TestMethodDFS()
        {
            Graphe graphe = new Graphe();
            graphe.Noeuds.Clear();
            graphe.Lien.Clear();

            graphe.Noeuds.Add(new Noeud(1));
            graphe.Noeuds.Add(new Noeud(2));
            graphe.Noeuds.Add(new Noeud(3));
            graphe.Noeuds.Add(new Noeud(4));

            graphe.AjouterLienPublic(1, 2);
            graphe.AjouterLienPublic(1, 3);
            graphe.AjouterLienPublic(1, 4);
            graphe.AjouterLienPublic(3, 4);


            StringWriter sortieConsole = new StringWriter();
            Console.SetOut(sortieConsole);

            graphe.DFS(3);

            string test = sortieConsole.ToString().Replace("\n", " ").Trim();

            Console.WriteLine(test); 
 
            Assert.IsTrue(test.Contains("D�but du parcours en profondeur � partir du noeud 3 :"));
            Assert.IsTrue(test.Contains("Visite du noeud 3"));
            Assert.IsTrue(test.Contains("Visite du noeud 1"));
            Assert.IsTrue(test.Contains("Visite du noeud 2"));
            Assert.IsTrue(test.Contains("Visite du noeud 4"));
            Assert.IsTrue(test.Contains("Fin"));
        }

    
        [TestMethod]
        /// teste la methode DFS avec un nouveau graphe (en convertissant la sortie console en string)

        public void TestMethodBFS()
        {
            Graphe graphe = new Graphe();
            graphe.Noeuds.Clear();
            graphe.Lien.Clear();

            graphe.Noeuds.Add(new Noeud(1));
            graphe.Noeuds.Add(new Noeud(2));
            graphe.Noeuds.Add(new Noeud(3));
            graphe.Noeuds.Add(new Noeud(4));

            graphe.AjouterLienPublic(1, 2);
            graphe.AjouterLienPublic(1, 3);
            graphe.AjouterLienPublic(1, 4);
            graphe.AjouterLienPublic(3, 4);



            StringWriter sortieConsole = new StringWriter();
            Console.SetOut(sortieConsole);

            graphe.BFS(3);

            string test = sortieConsole.ToString();

            Assert.IsTrue(test.Contains("\n\nParcours en largeur � partir du noeud 3 :"));
            Assert.IsTrue(test.Contains("3 1 4 2 "));
        }

    }
}