namespace TVShowTraker.Helpers.Exceptions
{
    public static class ExceptionMessages
    {
        public static readonly string Success = "Success";
        public static readonly string Fail = "Fail";
        public static readonly string ModelCreateSuccess = "{0} created successfully";
        public static readonly string ModelUpdateSuccess = "{0} updated successfully";
        public static readonly string ModelDeleteSuccess = "{0} deleted successfully";
        public static readonly string ModelIdInvalid = "{0} Id not valid";
        public static readonly string ModelNotExist = "{0} not exist";
        public static readonly string ModelNotCreatedDueToAlreadyExistInDB = "{0} not created, there is already in database";
        public static readonly string ModelAlreadyExist = "{0} already exist";
        public static readonly string ModelCreateError = "Error creating {0}";
        public static readonly string ModelUpdateError = "Error Updatign {0}";
        public static readonly string ModelDeleteError = "Error deleting {0}";
        public static readonly string FavouritAddedSuccessfully = "TVShow added successfully to your favourits";
        public static readonly string FavouritAlreadyExist = "TVShow already exist in your favourits";
        public static readonly string FavouritRemovedSuccessfully = "TVShow removed successfully from your favourits";
        public static readonly string AllFavouritsRemovedSuccessfully = "All favourits removed successfully";
    }
}
