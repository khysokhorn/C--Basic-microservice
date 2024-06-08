namespace Model
{
    public class MogoDBSetting
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string DataBaseName { get; set; }
        public string Password { get; set; }
        public string ConnectionString => $"mongodb+srv://{UserName}:{Password}@moviester-cluster.92h0o9a.mongodb.net/";
    }
}