namespace MSFD
{
    /// <summary>
    /// Data Values Class 
    /// SName - means Slash + Name
    /// </summary>
    public static class DV
    {
        public static string CURRENCY_GOLD_PATH = "/Gold";
        public static string CURRENCY_GEM_PATH = "/Gem";
        public static string REWARD_BUY_COUNT_PATH = "/Reward_Count"; // Depend on cost of lootbox
        public static string LOCALIZATION_PATH = "/Localization";
        public static string CURRENT_ENERGY_PATH = "/Energy";
        public static string LAST_ENERGY_TIME = "/EnergyTime";

        /// <summary>
        /// arg0 - path to D_Purchase
        /// </summary>
        public static string STRING_BUY_REQUEST = "STRING_BUY_REQUEST";
        /// <summary>
        /// arg0 - path to D_Upgrade
        /// </summary>
        public static string STRING_UPGRADE_REQUEST = "STRING_UPGRADE_REQUEST";
        public static string FLOAT_FLOAT_GEM_TO_GOLD_REQUEST = "FLOAT_FLOAT_GEM_TO_GOLD_REQUEST";
       
        //Localization File links
        public static string LOCALIZATION_ENGLISH_FILE = "LocalizedTextDataEN.json";
        public static string LOCALIZATION_RUSSIAN_FILE = "LocalizedTextDataRU.json";
        
        public static string BOUGHT_SUCCESS = "BOUGHT_SUCCESS";
        public static string BOUGHT_FAILED = "BOUGHT_FAILED";

        public static string LOAD_PREVIUOUS_GAME_SESSION = "LOAD_PREVIUOUS_GAME_SESSION";
        public static string CANCEL_PREVIOUS_GAME_SESSION = "CANCEL_PREVIOUS_GAME_SESSION";
        public static string STRING_FLOAT_UPGRADE_REQUEST = "STRING_FLOAT_UPGRADE_REQUEST";
        public static string STRING_PREVIEW_TRASNPORT_REQUEST = "STRING_PREVIEW_TRASNPORT_REQUEST";
        public static string CURRENT_PREVIEW_TRANSPORT = "CURRENT_PREVIEW_TRANSPORT";
        public static string STRING_SELECT_TRASNPORT_REQUEST = "STRING_SELECT_TRASNPORT_REQUEST";

        public static string STRING_REWARD_TYPE_GET_REWARD = "STRING_REWARD_TYPE_GET_REWARD";
        public static string RANDOM_REWARD_REQUEST = "RANDOM_REWARD_REQUEST";
        public static string RANDOM_BONUS_REWARD_REQUEST = "RANDOM_BONUS_REWARD_REQUEST";
        
        public static string FLOAT_REWARD_CURRENCY_GET = "FLOAT_REWARD_CURRENCY_GET";
        
        public static string UPGRADE_FAILED = "UPGRADE_FAILED";
        public static string UPGRADE_SUCCESS = "UPGRADE_SUCCESS";

        public static string CURRENCY_FLOAT_GIVE_REWARD_REQUEST = "CURRENCY_FLOAT_GIVE_REWARD_REQUEST";


        public static string tractorsName = "Tractors/";
        public static string skillsName = "Skills";

        public static string angelName = "Angel";
        public static string demonName = "Demon";
        //Different data 
        // SName- means Slash + Name
        public static string currencyName = "Currency";
        public static string currencySName = "/" + currencyName;

        public static string purchaseName = "Purchase";
        public static string purchaseSName = "/" + purchaseName;

        public static string upgradeName = "Upgrade";
        public static string upgradeSName = "/Upgrade";
        public static string purchaseTimeName = "/PurchaseTime";
        public static string passedTimeInSecondsName = "/PassedTime";
        public static string upgradeTypeName = "/UpgradeType";
        public static string descriptionName = "/Description";
        public static string assetsSName = "/Assets";
        public static string assetsName = "Assets";
        public static string costSName = "/Cost";
        public static string costName = "Cost";

        //In Assets Data Items
        public static string prefabInAssetsName = "Prefab";
        public static string iconInAssetsName = "Icon";

        public static string tractorsContainerName = "Tractors/";
        public static string bonusesContainerName = "Bonuses";

        //Upgrade times
        public static string installUpgradeTimesPath = "UpgradeTimes";
        public static string bonusesInstallTimeName = "/Bonus";
        public static string tractorInstallTimeName = "/Tractor";

        //Campaigns
        public static string campaignsPath = "Campaigns";
        public static string levelsCountInCampaign = "/LevelsCount";

        //RogueLite variables
        public static string rlIsLoadingAvailable = "Save/IsCanLoad";
        public static string rlCurrentCampaignPath = "Save/Campaign";
        public static string rlCurrentLevelIndexPath = "Save/LevelIndex";
        public static string rlInstalledSkillsPath = "Save/InstalledSkills";
        public static string rlHealthAmountPath = "Save/Health";
        public static string rlPetrolAmountPath = "Save/Petrol";
        public static string rlScoreAmountPath = "Save/Score";
        public static string rlCurrentTractorPath = "Save/Tractor";
        public static string rlCurrentSessionGoldPath = "Save/CurrentSessionGold";
        public static string rlEarnedGemsPath = "Save/EarnedGems";
        public static string rlKillsPath = "Save/Kills";
        public static string rlCanContinue = "Save/CanContinue";
        public static string rlLanguage = "Save/Language";
        public static string rlSessionKills = "Save/SessionKills";
        
        //Language
        public static string russian = "Russian";
        public static string english = "English";
        //Constants
        public static string trueConstantName = "true";
        public static string falseConstantName = "false";
    }
}