# Session 4 - The Event Bus

(120 min = 2h)

What if the GUI is composed of multiple user controls, each one with its own View Model. If one user control is changing the state of the business, the others must be notified. How can we do that? Let's implement and use a event bus?

## Create user controls for each part of the GUI (40 min)

- `InputTimeSection`
- `InputValueSection`
- `OutputTimeSection`
- `OutputValueSection`

## Create the Event Bus mechanism (20 min)

- The pub-sub pattern.
- Create the `EventBus` class.
- Extract it in a separate component called: `Infrastructure`

## Subscribe to the needed events from the view models (30 min)

- `MainViewModel`
- `InputTimeSection`
- `InputValueSection`
- `OutputTimeSection`
- `OutputValueSection`

## Raise events from the needed use cases (30 min)

- `Initialize`
- `SetInputTime`
- `SetOutputTime`
