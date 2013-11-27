using Cirrious.MvvmCross.Binding.BindingContext;
using CodeFramework.iOS.ViewControllers;
using CodeFramework.iOS.Views;
using CodeHub.Core.ViewModels.User;
using MonoTouch.Dialog;

namespace CodeHub.iOS.Views.User
{
    public class ProfileView : ViewModelDrivenViewController
    {
		public ProfileView()
		{
			Root.UnevenRows = true;
		}
        public override void ViewDidLoad()
        {
            Title = "Profile";

            base.ViewDidLoad();

            var vm = (ProfileViewModel) ViewModel;
			var header = new HeaderView();
            var set = this.CreateBindingSet<ProfileView, ProfileViewModel>();
            set.Bind(header).For(x => x.Title).To(x => x.Username).OneWay();
            set.Bind(header).For(x => x.Subtitle).To(x => x.User.Name).OneWay();
			set.Bind(header).For(x => x.ImageUri).To(x => x.User.AvatarUrl).OneWay();
            set.Apply();

			var followers = new StyledStringElement("Followers".t(), () => vm.LoadCommand.Execute(null), Images.Heart);
            var following = new StyledStringElement("Following".t(), () => vm.GoToFollowingCommand.Execute(null), Images.Following);
            var events = new StyledStringElement("Events".t(), () => vm.GoToEventsCommand.Execute(null), Images.Event);
            var organizations = new StyledStringElement("Organizations".t(), () => vm.GoToOrganizationsCommand.Execute(null), Images.Group);
            var repos = new StyledStringElement("Repositories".t(), () => vm.GoToRepositoriesCommand.Execute(null), Images.Repo);
            var gists = new StyledStringElement("Gists", () => vm.GoToGistsCommand.Execute(null), Images.Script);

			Root.Add(new [] { new Section(header), new Section { events, organizations, followers, following }, new Section { repos, gists } });
        }
    }
}
