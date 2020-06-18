namespace HP.Json

module Models =

    // Discriminated Union
    type Gender =
        | Male
        | Female
        | Other of string

    // This is a classical Enum
    type CustomerType =
        | Free = 0
        | Paid = 1
        | Premium = 2
