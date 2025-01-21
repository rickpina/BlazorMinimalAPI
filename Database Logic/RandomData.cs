namespace BlazorMinimalAPI.Database_Logic
{
    public static class RandomData
    {
        public static readonly List<string> FirstNames = new()
        {
            "James", "Mary", "Robert", "Patricia", "John", "Jennifer", "Michael",
            "Linda", "William", "Elizabeth", "David", "Barbara", "Richard", "Susan",
            "Joseph", "Jessica", "Thomas", "Sarah", "Charles", "Karen", "Rick", "Taylor",
            "Aeliana", "Thalion", "Lirael", "Kaelen", "Elowen", "Galadriel", "Eldrin",
            "Fendris", "Ysolde", "Galen", "Seraphine", "Alistair", "Elysia", "Kaelen",
            "Zarael", "Raelen", "Ilyana", "Aric", "Thalindra", "Darien", "Vaelen",
            "Astra", "Sorin", "Nymeria", "Cairn", "Emberlyn", "Valdir", "Isolde",
            "Fenris", "Alaric", "Solara", "Eldric", "Malin", "Lorien", "Tavira", 
        };

        public static readonly List<string> LastNames = new()
        {
            "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller",
            "Wilson", "Moore", "Taylor", "Anderson", "Thomas", "Jackson", "White",
            "Pina",
            "Stormrider", "Nightshade", "Ironfoot", "Silverwind", "Moonshadow", "Darkwater",
            "Firestone", "Redleaf", "Duskwhisper", "Swiftstrike", "Silverleaf", "Frostfall",
            "Windrunner", "Ashvale", "Starfire", "Dawnweaver", "Thundershield", "Blackthorn",
            "Dewmist", "Wildhammer", "Ironheart", "Skywhisper", "Grimstone", "Frostwind",
            "Brimstone", "Shadowbrook", "Whiteshadow", "Stonebloom", "Thalor", "Graysun"
        };

        public static readonly List<string> Occupations = new()
        {
            "Doctor", "Engineer", "Teacher", "Nurse", "Developer", "Artist",
            "Chef", "Pilot", "Lawyer", "Farmer", "Scientist", "Mechanic",
            "Software Developer",
            "Wizard", "Knight", "Sorcerer", "Thief", "Ranger",
            "Bard", "Paladin", "Druid", "Cleric", "Assassin",
            "Alchemist", "Enchanter", "Hunter", "Warlock", "Monk",
            "Barbarian", "Necromancer", "Archer", "Battle Mage", "Priest",
            "Warrior", "Mage", "Healer", "Seer", "Smith",
            "Scribe", "Seafarer", "Beastmaster", "Shaman", "Spymaster",
            "Elementalist", "Mercenary", "Pirate", "Scholar", "Artificer",
            "Runemaster", "Champion", "Cavalier", "Golemancer", "Thalassocrat",
            "Witch", "Summoner", "Tactician", "Dungeoneer", "Warden",
            "Tracker", "Beast Tamer", "Herbalist", "Illusionist", "Beastfolk Shaman",
            "Dragonrider", "Gladiator", "Archivist", "Arcane Investigator", "Shadowblade",
            "Vampire Hunter", "Runeweaver", "Moon Priestess", "Mystic", "Cursed One",
            "Demon Hunter", "Rune Knight", "Lightbringer", "Soulbinder", "Stormcaller"
        };

        public static readonly List<string> Titles = new()
        {
            "The Brave", "The Mighty", "The Fearless", "The Bold", "The Quick", 
            "The Swift", "The Fierce", "The Sharp", "The Clever", "The Cunning",
            "The Stealthy", "The Unstoppable", "The Vicious", "The Powerful",
            "The Ruthless", "The Savage", "The Wild", "The Loyal", "The Sly", "The Wise",
        };

        public static string GenerateFakeSSN()
        {
            var random = new Random();
            var ssn = $"{random.Next(100, 999)}-{random.Next(10, 99)}-{random.Next(1000, 9999)}";
            return ssn;
        }

        public static DateTime GenerateRandomDateOfBirth()
        {
            var random = new Random();
            // Set a range for the year (e.g., between 1950 and 2000)
            int year = random.Next(1950, 2000);

            // Set a random month (1 - 12)
            int month = random.Next(1, 13);

            // Set a random day, making sure it's valid for the selected month/year
            int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);

            return new DateTime(year, month, day);
        }

    }
}
