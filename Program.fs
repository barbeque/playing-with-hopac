open Hopac

// Job demo - run something asynchronously using the job computation expr

let doThingAsynchronously (getThingAsynchronously : Async<_>) (delay : Job<unit>) = job {
    let! result = getThingAsynchronously
    do! delay
    return result
}

let simpleAsyncDemo =
    let myResult = async { return 4 }
    let delay = timeOutMillis 10
    doThingAsynchronously myResult delay
        |> run
        |> printfn "Result was %i"

// Promise demo - a Promise is a "lazy" Job that only gets executed once
        
let sideEffect = job {
    printfn "> Side effect!"
    return 4
}

let demoWithoutPromises =
    printfn "Without memoization"
    run sideEffect |> printfn "Result 1 was %i"
    run sideEffect |> printfn "Result 2 was %i" // Side effect was called again here!

let demoWithPromises =
    printfn "With memoization"
    let memoized = memo sideEffect
    run memoized |> printfn "Result was %i"
    run memoized |> printfn "Result was %i" // Side effect should not be called again here

[<EntryPoint>]
let main argv = 
    simpleAsyncDemo
    demoWithoutPromises
    demoWithPromises
    0
