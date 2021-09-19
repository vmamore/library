namespace Library.Api.Infrastructure
{
    public class KeycloackSettings
    {
        public const string Path = "Keycloack";
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string LibraryRealmPath { get; set; }
    }
}
