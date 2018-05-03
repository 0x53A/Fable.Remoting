namespace Fable.Remoting.DotnetClient

open Quotations.Patterns

module Patterns =
    let (| AsyncField |_|) = function 
        | PropertyGet (Some (_), method, []) -> 
            Some(method.Name)
        | _ -> None 
    let (| NoArgs |_|) = function 
        | Lambda(_, AsyncField(methodName)) -> 
            Some (methodName, [ ])
        | _ -> None
    let (| OneArg |_|) = function 
        | Lambda(_, Application (AsyncField(methodName), Value((value, _)))) ->
            Some (methodName, [ value ])
        | _ -> None 
    let (| TwoArgs |_|) = function 
        | Lambda(_, Application (OneArg(methodName, args) , Value((arg, _)))) ->
            Some (methodName, [ yield! args; yield arg ]) 
        | _ -> None 
    let (| ThreeArgs |_|) = function 
        | Lambda(_, Application (TwoArgs(methodName, args) , Value((arg, _)))) ->
            Some (methodName, [ yield! args; yield arg ]) 
        | _ -> None 
    let (| FourArgs |_|) = function 
        | Lambda(_, Application (ThreeArgs(methodName, args) , Value((arg, _)))) ->
            Some (methodName, [ yield! args; yield arg ]) 
        | _ -> None 
    let (| FiveArgs |_|) = function 
        | Lambda(_, Application (FourArgs(methodName, args) , Value((arg, _)))) ->
            Some (methodName, [ yield! args; yield arg ]) 
        | _ -> None 

    let (| SixArgs |_|) = function 
        | Lambda(_, Application (FiveArgs(methodName, args) , Value((arg, _)))) ->
            Some (methodName, [ yield! args; yield arg ]) 
        | _ -> None 

    let (| SevenArgs |_|) = function 
        | Lambda(_, Application (SixArgs(methodName, args) , Value((arg, _)))) ->
            Some (methodName, [ yield! args; yield arg ]) 
        | _ -> None 
    let (| EightArgs |_|) = function 
        | Lambda(_, Application (SevenArgs(methodName, args) , Value((arg, _)))) ->
            Some (methodName, [ yield! args; yield arg ]) 
        | _ -> None 