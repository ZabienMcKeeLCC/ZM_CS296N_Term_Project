/*
 * 
 * These are the Mock Unit Tests. If I were able to get my Fake Repo to work with async methods,
 * these tests *should* run flawlessly.
 * 
 * using NUnit.Framework;
using ZM_CS296N_TermProject.Controllers;
using ZM_CS296N_TermProject.Models.DataLayer;
using ZM_CS296N_TermProject.Models.DomainModels;
using ZM_CS296N_TermProject.Models.ViewModels;

namespace CS296N_TermProject_Tests
{
    public class ReviewTests
    {
        FakeReviewRepo repo = new FakeReviewRepo();
        ReviewController controller = new ReviewController()

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CreateTest()
        {
            Review r = new Review()
            {
                Title = "Test",
                Message = "AAAAAAHHH UNIT TESTS AHHHHH",
                Date = "3/15/2022"
            };
            controller.Create(r);
            Assert.IsNotNull(repo.Find(r));
        }

        [Test]
        public void EditTest()
        {
            Review r = new Review()
            {
                Title = "Test",
                Message = "AAAAAAHHH UNIT TESTS AHHHHH",
                Date = "3/15/2022"
            };
            controller.Create(r);
            r.Message = "OOooooooOOOO UNIT TESTS!!!";
            controller.Edit(r.ReviewId, r);
            Assert.IsFalse(repo.SelectByIdAsync(r).Message == "AAAAAAHHH UNIT TESTS AHHHHH");

        }

        public void DeleteTest()
        {
            Review r = new Review()
            {
                ReviewId = 1,
                Title = "Test",
                Message = "AAAAAAHHH UNIT TESTS AHHHHH",
                Date = "3/15/2022"
            };
            controller.Create(r);
            controller.DeleteConfirmed(r.ReviewId);
            Assert.IsNull(repo.SelectByIdAsync(r.ReviewId));
        }

        public void WriteReplyTest()
        {
            Review r = new Review()
            {
                ReviewId = 1,
                Title = "Test",
                Message = "AAAAAAHHH UNIT TESTS AHHHHH",
                Date = "3/15/2022"
            };
            controller.Create(r);
            CommentVM commentVM = new CommentVM() { ReviewId = 1, Message = "EEEEEK UNIT TESTS OOOooooOOOOoo"};
            controller.WriteReply(commentVM);
            Assert.IsNotNull(repo.SelectByIdAsync(1).Comments);

        }
    }
}
*/