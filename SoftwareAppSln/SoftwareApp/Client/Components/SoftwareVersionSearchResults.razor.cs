using SoftwareApp.Shared;
using System.Text.RegularExpressions;

namespace SoftwareApp.Client.Components
{
    public partial class SoftwareVersionSearchResults
    {
        private string searchText = string.Empty;
        private bool searchTextValid = false;
        private string searchTextError = string.Empty;
        private List<Software> searchResults = new();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private void OnSearchClick()
        {
            if (!searchTextValid)
                return;

            Version version = new Version(searchText);
            searchResults = SoftwareManager.GetAllSoftware().Where(s => new Version(s.Version) > version).ToList();
        }

        private void OnEnterSearchText()
        {
            Regex regex = new Regex(@"^(\d+)((\.{1}\d+)*)(\.{0})$");
            searchTextValid = regex.IsMatch(searchText);
            if (!searchTextValid)
                searchTextError = "Incorrect version format.";
            else
                searchTextError = string.Empty;
        }
    }
}