# Inflation Calculator - The Course

The course is designed for a small group. Max 10 people.

Webcam is mandatory. Thank you.

## Steps

- Step I - Requirement analysis (focused on use cases)

- Step II - Implementation:

  - (1) WPF application with state kept in business layer.
    - GUI reflects the state of the business, but does not store any state by itself.

  - (2) Console application (stateless business application)
  - (3) Implement: Web service (stateless business application)
  - (4) Implement a WPF application with state in GUI
  - (5) Implement: WPF application with state kept in business layer and making usage of a Event Bus.

## Types of applications

| App Type          | State                   | Other Details  | Comments            |
| ----------------- | ----------------------- | -------------- | ------------------- |
| Console (desktop) | stateless               |                |                     |
| WPF (desktop)     | state in GUI            |                | not a good practice |
| WPF (desktop)     | state in business layer |                |                     |
| WPF (desktop)     | state in business layer | with Event Bus |                     |
| Web service (web) | stateless               |                |                     |
| Web service (web) | state in db             |                |                     |

