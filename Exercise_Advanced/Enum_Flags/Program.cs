namespace Enum_Flags
{
    internal class Program
    {
        [Flags]
        enum FileAttributes
        {
            ReadOnly = 1 << 0,
            Hidden = 1 << 1,
            System = 1 << 2,
            Archive = 1 << 3
        }

        [Flags]
        enum UserRoles
        {
            Guest = 1 << 0,
            Editor = 1 << 1,
            Admin = 1 << 2
        }

        [Flags]
        enum MachineStatus
        {
            PoweredOn = 1 << 0,
            Running = 1 << 1,
            Error = 1 << 2,
            MaintenanceMode = 1 << 3
        }

        [Flags]
        enum CoffeeToppings
        {
            None = 0,
            Milk = 1 << 0,
            Sugar = 1 << 1,
            Caramel = 1 << 2,
            FullFlavor = Milk | Sugar | Caramel //The Combo 
        }

        [Flags]
        enum NetworkProtocols
        {
            Protocol1 = 1 << 0,
            Protocol2 = 1 << 1,
            Protocol3 = 1 << 2,
            Protocol4 = 1 << 3,
            Protocol5 = 1 << 4,
            
        }

        static void Main(string[] args)
        {
            //1. The "Add & Remove" Pattern
            FileAttributes myFileAttribute = FileAttributes.ReadOnly;
            Console.WriteLine($"Currently myFileAttribute is: {myFileAttribute}");
            myFileAttribute = ToggleArchive(myFileAttribute);
            Console.WriteLine($"After ToggleArchive method triggered, myFileAttribute are:{myFileAttribute}");
            myFileAttribute = ToggleArchive(myFileAttribute);
            Console.WriteLine($"Toggle again and myFileAttribute is: {myFileAttribute}");

            //2. The "Permission Guard"
            UserRoles myUserRoles = UserRoles.Guest | UserRoles.Editor;
            Console.WriteLine($"Does the user got the right to edit? {CanEdit(myUserRoles)}");

            //3. Validation Logic
            MachineStatus myMachineStatus = MachineStatus.Running | MachineStatus.MaintenanceMode;
            Console.WriteLine($"Is the machine having valid status? {IsValidState(myMachineStatus)}");

            //4. The "Bulk Header"
            CoffeeToppings myCoffee = CoffeeToppings.Milk | CoffeeToppings.Sugar | CoffeeToppings.Caramel;
            Console.WriteLine($"Coffee grade: {CoffeeGrade(myCoffee)}");

            //5. Binary Math (Power of Two)
            Console.WriteLine($"Network protocol of {nameof(NetworkProtocols.Protocol5)} is having value of 16? {((int)NetworkProtocols.Protocol5).Equals(16)}");
            
        }

        static FileAttributes ToggleArchive(FileAttributes current)
        {
            return current ^ FileAttributes.Archive;
        }

        static bool CanEdit(UserRoles user)
        {
            return (user.HasFlag(UserRoles.Admin) || user.HasFlag(UserRoles.Editor));
        }

        static bool IsValidState(MachineStatus status)
        {
            bool isRunning = (status & MachineStatus.Running) == MachineStatus.Running;
            bool isMaintenance = (status & MachineStatus.MaintenanceMode) == MachineStatus.MaintenanceMode;

            return !(isRunning && isMaintenance);
        }

        static string CoffeeGrade(CoffeeToppings coffee)
        {
            bool isFullFlavor = (coffee & CoffeeToppings.FullFlavor) == CoffeeToppings.FullFlavor;
            CoffeeToppings standard = CoffeeToppings.Milk | CoffeeToppings.Sugar;
            bool isStandard = (coffee & (CoffeeToppings.Milk | CoffeeToppings.Sugar)) == standard;

            if (isFullFlavor)
                return "Full House";
            else if (isStandard)
                return "Standard";
            else
                return "Unknown";
        }
    }
}
