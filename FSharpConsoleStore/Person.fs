module FSharpConsoleStore.Person

[<AllowNullLiteral>]
type public Person (name, surname, socialSecurityNumber) =
    member val Name: string = name with get, set
    member val Surname: string = surname with get, set
    member val SocialSecurityNumber: string = socialSecurityNumber with get, set
    
    override this.ToString() =
        $"Person: Name: {this.Name}, Surname: {this.Surname}, SocialSecurityNumber: {this.SocialSecurityNumber}"