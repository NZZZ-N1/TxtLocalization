Before using it,You must create a instance of SavingInfo and LocalizationInfo

static void Foo()
{
  new SavingInfo("Your direction", "txt");
  new LocalizationInfo("Your direction", ["en", "cn", "rs"]);

  string str = Localization.GetContent("Your key");
  str = "Your key".Localized();
}
