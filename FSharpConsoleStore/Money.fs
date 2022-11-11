module FSharpConsoleStore.Money

open System

type public Money(Amount, Currency) =
    member val Amount: Decimal = Amount with get, set
    member val Currency: string = Currency with get, set
    
    member this.ToPLN() =
        match this.Currency with
        | "PLN" -> this
        | "EUR" -> Money(this.Amount * 4.68m, "PLN")
        | _ -> failwith "Unknown currency"
       
    member this.ToEUR() =
        match this.Currency with
        | "EUR" -> this
        | "PLN" -> Money(this.Amount * 0.21m, "EUR")
        | _ -> failwith "Unknown currency"
    
    static member (+) (a: Money, b: Money) =
        let a = a.ToPLN()
        let b = b.ToPLN()
        Money(a.Amount + b.Amount, "PLN")
        
    static member get_Zero() = Money(0m, "PLN")
        
    override this.ToString() =
        $"{this.Amount} {this.Currency}"
        
    override this.GetHashCode()
        = this.Amount.GetHashCode() + this.Currency.GetHashCode()
    override this.Equals(o) =
        let other = o :?> Money
        this.Amount = other.Amount && this.Currency = other.Currency
            
    interface IComparable with
        member this.CompareTo(o) =
            let other = o :?> Money
            this.ToPLN().Amount.CompareTo(other.ToPLN().Amount)
        