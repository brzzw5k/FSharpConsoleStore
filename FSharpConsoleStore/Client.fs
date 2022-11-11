module FSharpConsoleStore.Client

open System
open FSharpConsoleStore.Person
open FSharpConsoleStore.Money
open FSharpConsoleStore.LoyaltyStatus

type public Client (Id, Person, CreditLimit, RegistrationDate, LoyaltyStatus) =
    member val Id: int = Id with get, set
    member val Person: Person = Person with get, set
    member val CreditLimit: Money = CreditLimit with get, set
    member val RegistrationDate: DateTime = RegistrationDate with get, set
    member val LoyaltyStatus: LoyaltyStatus = LoyaltyStatus with get, set
    
    new (Id, name, surname, socialSecurityNumber, creditLimit, registrationDate, loyaltyStatus) = 
        Client(Id, Person(name, surname, socialSecurityNumber), creditLimit, registrationDate, loyaltyStatus)

    new (Id, creditLimit, registrationDate, loyaltyStatus) = 
        Client(Id, null, creditLimit, registrationDate, loyaltyStatus)
    override this.ToString() =
        $"Client: Id: {this.Id} Person: {this.Person} CreditLimit: {this.CreditLimit} RegistrationDate: {this.RegistrationDate} LoyaltyStatus: {this.LoyaltyStatus}"
        