using System;
using System.Collections.Generic;

public class Ticket
{
    public int SerialNumber { get; set; }
    public string User { get; set; }
    public string Location { get; set; }
    public string TelNumber { get; set; }
    public string EmailId { get; set; }
    public DateTime OpenedDateTime { get; set; }
    public string CaseDescription { get; set; }
    public string Agent { get; set; }
    public DateTime? ResolvedDateTime { get; set; }

}

public class TicketSystem
{
    private Queue<Ticket> WaitingListQueue;
    private LinkedList<Ticket> assignedList;

    public TicketSystem()
    {
        WaitingListQueue = new Queue<Ticket>();
        assignedList = new LinkedList<Ticket>();
    }

    public void AddTicket(Ticket ticket)
    {

        ticket.OpenedDateTime = DateTime.Now;
        WaitingListQueue.Enqueue(ticket);
    }


    public void DeleteTicketToAssignAgent()
    {
        if (WaitingListQueue.Count > 0)
        {
            Ticket ticket = WaitingListQueue.Dequeue();
            ticket.Agent = "Assigned Agent"; // Set the assigned agent

            AddToAssignedList(ticket);
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine("Succesfully assigned an agent to the ticket");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------");
        }
    }

    public void Edit_Ticket(int serialNumber, Ticket updatedTicket)
    {
        // Create a new ticket with updated details
        Ticket newTicket = new Ticket
        {
            SerialNumber = updatedTicket.SerialNumber,
            User = updatedTicket.User,
            Location = updatedTicket.Location,
            TelNumber = updatedTicket.TelNumber,
            EmailId = updatedTicket.EmailId,
            OpenedDateTime = DateTime.Now,
            CaseDescription = updatedTicket.CaseDescription,
            Agent = "",
            ResolvedDateTime = null
        };

        AddTicket(newTicket);
    }

    public void ResolveTicket(int serialNumber)
    {
        // Find the ticket in the assigned list
        var node = assignedList.First;

        while (node != null)
        {
            if (node.Value.SerialNumber == serialNumber)
            {
                // Set the resolved date and time
                node.Value.ResolvedDateTime = DateTime.Now;
                break;
            }

            node = node.Next;
        }
    }

    public void DisplayWaitingList()
    {
        Console.WriteLine("Waiting List:");
        Console.WriteLine("**************************************************************************************************************************");

        foreach (var ticket in WaitingListQueue)
        {
            DisplayTicketDetails(ticket);
            Console.WriteLine("**************************************************************************************************************************");
        }
    }

    public void DisplayAssignedList()
    {
        Console.WriteLine("Assigned List:");
        Console.WriteLine("**************************************************************************************************************************");
        foreach (var ticket in assignedList)
        {
            DisplayTicketDetails(ticket);
            Console.WriteLine("**************************************************************************************************************************");
        }
    }

    private void AddToAssignedList(Ticket ticket)
    {
        // Add to the assigned list based on the Opened Date and Time
        var node = assignedList.First;

        while (node != null && node.Value.OpenedDateTime <= ticket.OpenedDateTime)
        {
            node = node.Next;
        }

        if (node == null)
        {
            assignedList.AddLast(ticket);
        }
        else
        {
            assignedList.AddBefore(node, ticket);
        }
    }

    private void DisplayTicketDetails(Ticket ticket)
    {
        Console.WriteLine($"Serial Number: {ticket.SerialNumber}");
        Console.WriteLine($"User: {ticket.User}");
        Console.WriteLine($"Location: {ticket.Location}");
        Console.WriteLine($"Tel Number: {ticket.TelNumber}");
        Console.WriteLine($"Email Id: {ticket.EmailId}");
        Console.WriteLine($"Opened Date and Time: {ticket.OpenedDateTime}");
        Console.WriteLine($"Case Description: {ticket.CaseDescription}");
        Console.WriteLine($"Agent: {ticket.Agent}");
        Console.WriteLine($"Resolved Date and Time: {ticket.ResolvedDateTime}");
        Console.WriteLine();
    }

    public void SelectOption()
    {

        Console.WriteLine("Select an option:");
        Console.WriteLine("1.Open a ticket");
        Console.WriteLine("2.Assign an agent");
        Console.WriteLine("3.Edit a ticket");
        Console.WriteLine("4.Resolve a ticket");
        Console.WriteLine("5.Display waiting list");
        Console.WriteLine("6.Display assigned list");
        Console.WriteLine("7.Exit");

        int option = int.Parse(Console.ReadLine());

        switch (option)
        {
            case 1:
                // Open a ticket
                Console.WriteLine("Enter ticket details:");
                Ticket ticket = new Ticket();
                Console.Write("Serial Number: ");
                ticket.SerialNumber = int.Parse(Console.ReadLine());
                Console.Write("User: ");
                ticket.User = Console.ReadLine();
                Console.Write("Location: ");
                ticket.Location = Console.ReadLine();
                Console.Write("Tel Number: ");
                ticket.TelNumber = Console.ReadLine();
                Console.Write("Email Id: ");
                ticket.EmailId = Console.ReadLine();
                Console.Write("Case Description: ");
                ticket.CaseDescription = Console.ReadLine();

                AddTicket(ticket);
                break;

            case 2:
                // Assign an agent
                DeleteTicketToAssignAgent();
                break;

            case 3:
                // Edit a ticket
                Console.Write("Enter the serial number of the ticket to edit: ");
                int serialNumber = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter updated ticket details:");
                Ticket updatedTicket = new Ticket();
                Console.Write("User: ");
                updatedTicket.User = Console.ReadLine();
                Console.Write("Location: ");
                updatedTicket.Location = Console.ReadLine();
                Console.Write("Telphone Number: ");
                updatedTicket.TelNumber = Console.ReadLine();
                Console.Write("Email Id: ");
                updatedTicket.EmailId = Console.ReadLine();
                Console.Write("Case Description: ");
                updatedTicket.CaseDescription = Console.ReadLine();

                Edit_Ticket(serialNumber, updatedTicket);
                break;

            case 4:
                // to Resolve a ticket
                Console.Write("Enter the serial number of the ticket to resolve: ");
                int resolveSerialNumber = int.Parse(Console.ReadLine());
                ResolveTicket(resolveSerialNumber);
                break;

            case 5:
                // to Display waiting list
                DisplayWaitingList();
                break;

            case 6:
                // to Display assigned list
                DisplayAssignedList();
                break;

            case 7:
                // to Exit
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }

        SelectOption();
    }
}
