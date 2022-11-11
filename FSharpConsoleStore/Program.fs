module Program

open System
open FSharpConsoleStore
open FSharpConsoleStore.LoyaltyStatus
open FSharpConsoleStore.Client
open FSharpConsoleStore.Money
open FSharpConsoleStore.Transaction
open FSharpConsoleStore.Shop
let MainMenu() =
    Console.WriteLine("Please select an option:")
    Console.WriteLine("1. View client information")
    Console.WriteLine("2. Increase client credit")
    Console.WriteLine("3. Verify client credit")
    Console.WriteLine("4. Promote client to VIP")
    Console.WriteLine("5. Exit")
    
let ViewClientInfo(shop: Shop) =
    Console.WriteLine("Please enter client id:")
    let input = Console.ReadLine()
    try
        let id = int input
        let client = shop.GetClient(id)
        Console.WriteLine(client.ToString())
    with
        | :? System.Collections.Generic.KeyNotFoundException -> Console.WriteLine("Client not found")
        | :? System.FormatException -> Console.WriteLine("Invalid input")
        
let IncreaseClientCredit(shop: Shop) =
    Console.WriteLine ("Please enter client id:")
    let input = Console.ReadLine()
    try
        let id = int input
        let client = shop.GetClient(id)
        Console.WriteLine("Please enter amount to increase credit by:")
        let input = Console.ReadLine()
        let amount = decimal input
        Console.WriteLine("Please enter currency:")
        let input = Console.ReadLine()
        let currency = if input = "PLN" then "PLN" else if input = "EUR" then "EUR" else null
        shop.IncreaseClientCredit(id, Money(amount, currency))
        Console.WriteLine("Credit increased")
    with
        | :? System.Collections.Generic.KeyNotFoundException -> Console.WriteLine("Client not found")
        | :? System.FormatException -> Console.WriteLine("Invalid input")
        | :? System.Exception as ex -> Console.WriteLine(ex.Message)
        
let VerifyClientCredit(shop: Shop) =
    Console.WriteLine("Please enter client id:")
    let input = Console.ReadLine()
    try
        let id = int input
        let client = shop.GetClient(id)
        let sumOfTransactions = shop.GetSumOfUnpaidTransactions(id)
        if sumOfTransactions > client.CreditLimit then
            Console.WriteLine("Client credit exceeded")
        else
            Console.WriteLine("Client credit not exceeded")
    with
        | :? System.Collections.Generic.KeyNotFoundException -> Console.WriteLine("Client not found")
        | :? System.FormatException -> Console.WriteLine("Invalid input")
    
let PromoteClientToVIP(shop: Shop) =
    Console.WriteLine("Please enter client id:")
    let input = Console.ReadLine()
    try
        let id = int input
        shop.PromoteClient(id)
        Console.WriteLine("Client promoted to VIP")
    with
        | :? System.Collections.Generic.KeyNotFoundException -> Console.WriteLine("Client not found")
        | :? System.FormatException -> Console.WriteLine("Invalid input")
        | :? System.Exception as ex -> Console.WriteLine(ex.Message)
    
let rec MainLoop(shop: Shop) state =
    MainMenu()
    let input = Console.ReadLine()
    match input with
    | "1" -> ViewClientInfo(shop); MainLoop(shop) state
    | "2" -> IncreaseClientCredit(shop); MainLoop(shop) state
    | "3" -> VerifyClientCredit(shop); MainLoop(shop) state
    | "4" -> PromoteClientToVIP(shop); MainLoop(shop) state
    | "5" -> None
    | _ -> Console.WriteLine("Invalid input"); MainLoop(shop) state
    
    
let GenerateTestShop() =
    let client1 = Client(1, Money(250.0m, "PLN"), DateTime(2021, 1, 1), LoyaltyStatus.Normal)
    let client2 = Client(2, "Jane", "Doe", "09876543210", Money(1000.0m, "PLN"), DateTime(2021, 1, 1), LoyaltyStatus.Normal)
    let client3 = Client(3, "John", "Foo", "01234567890", Money(1000.0m, "PLN"), DateTime(2022, 1, 1), LoyaltyStatus.VIP)
    let client4 = Client(4, "Jane", "Bar", "01234567890", Money(1000.0m, "PLN"), DateTime(2021, 1, 1), LoyaltyStatus.Normal)
    let clients = [client1; client2; client3; client4]
    
    let transaction1 = Transaction(1, Money(100.0m, "EUR"), false)
    let transaction2 = Transaction(1, Money(200.0m, "PLN"), true)
    let transaction3 = Transaction(2, Money(300.0m, "PLN"), false)
    let transaction4 = Transaction(2, Money(400.0m, "PLN"), false)
    let transaction5 = Transaction(3, Money(500.0m, "PLN"), false)
    let transaction6 = Transaction(3, Money(600.0m, "PLN"), false)
    let transaction7 = Transaction(4, Money(700.0m, "PLN"), true)
    let transaction8 = Transaction(4, Money(800.0m, "PLN"), true)
    let transactions = [transaction1; transaction2; transaction3; transaction4; transaction5; transaction6; transaction7; transaction8]
    
    Shop(clients, transactions)
    
[<EntryPoint>]
let main argv =
    let shop = GenerateTestShop()
    ignore (MainLoop(shop) None)
    0 // return an integer exit code