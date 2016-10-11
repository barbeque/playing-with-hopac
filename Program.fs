open Hopac

let doThingAsynchronously (getThingAsynchronously : Async<_>) (delay : Job<unit>) = job {
    let! result = getThingAsynchronously
    do! delay
    return result
}

[<EntryPoint>]
let main argv = 
    let myResult = async { return 4 }
    let delay = timeOutMillis 10
    doThingAsynchronously myResult delay
        |> run
        |> printfn "Result was %i"
    0
