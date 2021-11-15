using Microsoft.VisualStudio.TestTools.UnitTesting;
using modul02_tema01;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public MusicRepository testRepo;
        [TestMethod]
        public void TestCreation()
        {

            testRepo = new MusicRepository("non existent");
            testRepo = new MusicRepository("media");
        }

        [TestMethod]
        public void TestAddition()
        {

            TestCreation();
            string[] faultyParams = { "1", "2", "3", "4", "5", "6" };

            Media media = new Media(faultyParams);
            testRepo.Insert(media);
            media = new Media(faultyParams);
            testRepo.Insert(media);
        }

        [TestMethod]
        public void TestDeletion()
        {
            TestCreation();
            TestAddition();

            testRepo.Delete(1);
            testRepo.Delete(1);

        }
    }
}
