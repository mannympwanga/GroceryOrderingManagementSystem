using System;

namespace GroceryOrderingManagementSystem
{
    public static class UserSession
    {
        //=====================================================
        // CURRENT USER DETAILS
        //=====================================================
        public static string LoginName { get; set; } = string.Empty;
        public static string Username { get; set; } = string.Empty;
        public static string MemberID { get; set; } = string.Empty;
        public static string Role { get; set; } = string.Empty;

        public static bool IsAdministrator =>
            string.Equals(Role, "Admin", StringComparison.OrdinalIgnoreCase)
            || string.Equals(Role, "Matron", StringComparison.OrdinalIgnoreCase);

        public static bool IsMember =>
            string.Equals(Role, "Member", StringComparison.OrdinalIgnoreCase);

        //=====================================================
        // SESSION RESET
        //=====================================================
        public static void Clear()
        {
            LoginName = string.Empty;
            Username = string.Empty;
            MemberID = string.Empty;
            Role = string.Empty;
        }
    }
}
