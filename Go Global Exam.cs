/*
Part 1 – C# Code
1.
Please describe what issue you can see in IncludeInFinalResults method
and how you would refactor this method to solve the issue.
*/

public enum RoomAvailableOption
{
    All,
    AvailableOnly,
    NotAvailableOnly
}

public class RoomSearchFilter
{
    RoomAvailableOption enforcedOption;

    public RoomSearchFilter(RoomAvailableOption enforcedOption)
    {
        this.enforcedOption = enforcedOption;
    }

    public bool IncludeInFinalResults(Room room)
    {
        bool isAvailable = CheckIfRoomAvailable(room);
        
        switch (enforcedOption)
        {
            case enforcedOption.All: // enum name is "RoomAvailableOption". (solution: replace "enforceOption" with "RoomAvailableOption")
                return true;
            case enforcedOption.AvailableOnly: 
                return isAvailable;
            case enforcedOption.NotAvailableOnly: 
                return isAvailable; // needs to return that the room is not available. (solution: something like !isAvailable)
            default: 
                return true;
        }
    }
}




/*
2. Given the following scenario, design a class structure using OOP principles:
- You are developing a ride-sharing application.
You need classes for `Driver`, `Passenger`, `Ride’, and `Vehicle`.
Each ride should be associated with a driver and a passenger.
Ensure proper encapsulation and use of inheritance/polymorphism where applicable.
*/




// base class for all the users, after that we'll be able to inherit 
public abstract class User
{
    // getters and setters
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string PhoneNumber { get; set; }
    
    public double Rating { get; private set; }

    public User(string id, string name, string phoneNumber)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        Rating = 5.0; // set default rating at 5.0
    }
    public virtual string GetProfileSummary()
    {
        return $"User: {Name}, Rating: {Rating}";
    }
}


// Passenger class inherits from User class
public class Passenger : User
{
    public string PaymentMethod { get; private set; }

    public Passenger(string id, string name, string phoneNumber, string paymentMethod) 
        : base(id, name, phoneNumber)
    {
        PaymentMethod = paymentMethod;
    }


    // Overrid the base method to get specific passenger details
    public override string GetProfileSummary()
    {
        return $"Passenger: {Name}, Pays with: {PaymentMethod}";
    }
}


public class Vehicle
{
    public string LicensePlate { get; private set; }
    public string Model { get; private set; }

    public Vehicle(string licensePlate, string model)
    {
        LicensePlate = licensePlate;
        Model = model;
    }
}


// Driver class inherits from User class
public class Driver : User
{
    public string LicenseNumber { get; private set; }
    
    // Composition:dreiver has a vehicle
    public Vehicle DriverVehicle { get; private set; }

    public Driver(string id, string name, string phoneNumber, string licenseNumber, Vehicle vehicle) 
        : base(id, name, phoneNumber)
    {
        LicenseNumber = licenseNumber;
        DriverVehicle = vehicle;
    }

}

// enum: ride status
public enum RideStatus
{
    Requested, InProgress, Completed
}

// Ride class connects the Driver, Passenger, and Ride details
public class Ride
{
    public string RideId { get; private set; }
    public Passenger RidePassenger { get; private set; }
    public Driver RideDriver { get; private set; }
    public string StartPoint { get; private set; }
    public string EndPoint { get; private set; }
    
    public RideStatus Status { get; private set; }

    public Ride(string rideId, Passenger passenger, Driver driver, string startPoint, string endPoint)
    {
        RideId = rideId;
        RidePassenger = passenger;
        RideDriver = driver;
        StartPoint = startPoint;
        EndPoint = endPoint;
        Status = RideStatus.Requested;
    }


    public void StartRide()
    {
        if (Status == RideStatus.Requested)
        {
            Status = RideStatus.InProgress;
            Console.WriteLine($"Ride {RideId} has started.");
        }
    }

    public void CompleteRide()
    {
        if (Status == RideStatus.InProgress)
        {
            Status = RideStatus.Completed;
            Console.WriteLine($"Ride {RideId} has been completed.");
        }
    }
}


/*
 3:

3. Which design pattern would you use in the following scenarios, and why?
a) Ensuring only one instance of a logging service exists throughout the application.
answer: singelton. we want to ensure that only one reference of instance of class. with logs, we would want that all og the classes will write to the same place
b) Decoupling the creation of complex objects from their representation.
answer: builder. we can build the object "step by step". if there are a lot of params to be sent to the constructor - it mighe lead to problems if we won't use builder pattern
c) Allowing new functionalities to be added to an object dynamically without modifying its structure.
answer: decorator. its like "wrap" the object with a method for example, without the use of inheritance (might be more methods that we dont want in this object)
d) Providing a simplified interface to a complex subsystem.
answer: idk :(

*/




/*
 4:
Explain the importance of SOLID principles and provide an example of a violation of the Open/Closed Principle. How would you fix it?
answer: idk
*/

/* 5:
Write a function to find the longest substring without repeating characters.
I'll use hash table
main idea: lets think a container.
it will be our sub string and will have 2 pointers - left and right.
we need to move the right pointer to the right and every new char - we'll add it to the hashset
it the char was at the hashset - move the left pointer ands delete the chars from the hashset untill the "double" char leaves the container
at every step - keep track of the length


public class SubstringFinder
{
    public int LengthOfLongestSubstring(string s)
    {
        if (string.IsNullOrEmpty(s)) return 0;

        HashSet<char> charSet = new HashSet<char>();
        int left = 0;
        int maxLength = 0;

        for (int right = 0; right < s.Length; right++)
        {
            while (charSet.Contains(s[right]))
            {
                charSet.Remove(s[left]);
                left++;
            }

            charSet.Add(s[right]);
            maxLength = Math.Max(maxLength, right - left + 1);
        }

        return maxLength;
    }
}

*/



/*

6. What are the key differences between SQL and NoSQL databases? When would you choose NoSQL over SQL?
answer: SQL has tables, relations, foreign keys while in NOSQL there are'nt tables and you can use different methods
to keep the data (Json,...).
I would use NOSQL in cases like: save an object with list inside of him. because in this case in SQL I'll need to combaine tables with JOINS.

*/





/*

PART 2: SQL

1.
SELECT ac.BANK_ID, ac.BRANCH_ID, ac.ACCOUNT_NUM, ac.CURRENCY, am.AMOUNT
FROM ACCOUNTS AS ac
JOIN AMOUNTS AS am ON CONCAT_WS(' ', ac.BANK_ID, ac.BRANCH_ID, ac.ACCOUNT_NUM) = am.ACCOUNT_REC;


3.
SELECT C.name, SUM(O.amount) AS total_amount
FROM Customers AS C
JOIN Orders AS O USING(customer_id)
WHERE O.order_date >= DATE_SUB(CURDATE(), INTERVAL 6 MONTH)
GROUP BY C.customer_id, C.name
ORDER BY total_amount DESC
LIMIT 3; // top 3 customers

4.
SELECT C.customer_id, C.name
FROM Customers C
JOIN Orders O USING(customer_id)
WHERE O.order_date >= DATE_SUB(CURDATE(), INTERVAL 1 YEAR) // orders from last year
GROUP BY C.customer_id, C.name
HAVING COUNT(DISTINCT DATE_FORMAT(O.order_date, '%Y-%m')) = 12; // we need 12 differnet months
*/

