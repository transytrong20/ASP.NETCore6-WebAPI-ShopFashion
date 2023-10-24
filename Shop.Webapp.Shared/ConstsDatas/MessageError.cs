namespace Shop.Webapp.Shared.ConstsDatas
{
    public static class MessageError
    {
        private const string ModelValidatorError = "Model.Error";
        private const string ActionError = "Action.Error";

        public const string NotEmpty = $"{ModelValidatorError}.NotEmpty";
        public const string UnValid = $"{ModelValidatorError}.IsNotValid";
        public const string Dupplicate = $"{ModelValidatorError}.Dupplicate";
        public const string DataNotFound = $"{ModelValidatorError}.NotFound";
        public const string FileEmpty = $"{ModelValidatorError}.FileEmpty";
        public const string PwdIsNotValid = $"{ModelValidatorError}.PasswordIsNotValid";

        public const string NotFound = $"{ActionError}.NotFound";
        public const string UnChange = $"{ActionError}.UnChanged";

        public const string FileCannotAddItem = $"{ActionError}.File_Cannot_Add_Item";
        public const string FileAvailabel = $"{ActionError}.File_Available_In_Directory";
        public const string DirectoryEmpty = $"{ActionError}.Directory_Is_Empty";

    }

    public static class ActionMessage
    {
        private const string Success = "Success";
        public const string Create = $"Create.{Success}";
        public const string Update = $"Update.{Success}";
        public const string Remove = $"Remove.{Success}";
    }
}
