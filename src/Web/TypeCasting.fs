namespace HP.Web

module Casting =

    // Casting classical enums
    type Color =
        | Red = 1
        | Green = 2
        | Blue = 3


    // Two ways to convert enums
    // First one will throw exception
    let color1 = enum<Color> 10
    let color: Color = enum 1

    // UPCASTING AND DOWNCASTING
    // Having OOP features, upcasing and downcasting are important for F#

    // UPCASTING
    // Three rules
    // 1. Upcasting is automatic when using methods
    // 2. Upcasting is NOT automatic in let-bound functions
    // 3. Using flexible types you can do upcasting in let-bound functions

    type Base() = class end

    type Derived() =
        inherit Base()


    let children: Derived = Derived()

    // Two ways to do upcasting (You can upcast to any level higher)
    // 1. Using ( :> ) operator
    // 2. Using  upcast operator
    let parent1: Base = children :> Base
    let parent2: Base = upcast children
    let parent3: obj = upcast children

    // You can also upcast the primitive type
    let str = "Hello"
    let strParent1 = str :> obj
    let strParent2: obj = upcast str

    // DOWNCASTING
    // Three ways
    // 1. Unsafe using ( :?> ) operator. Possibly throws exception
    // 2. Unsafe downcast operator. Possibly throws exception
    // 3. Safe `type cast` as follows

    // Unsafe downcasting. Throw exception at runtime
    let againTryString1: string = downcast parent3
    let againTryString2 = parent3 :?> string

    // Safe downcasting
    let hello =
        match strParent1 with
        | :? string as x -> x
        | _ -> "some default string"
