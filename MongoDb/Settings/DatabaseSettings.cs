namespace MongoDb.Settings
{
    public class DatabaseSettings:IDatabaseSettings //Interfaceden miras alıyoruz, class interfaceden metot bazında miras aldığında sorun çıkmazken property bazında miras aldığında sıkıntı çıkabiliyor, bu yüzden inteface içindeki propertyleri aynı şekilde buraya kaydediyoruz.
    {
        public string CategoryCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
