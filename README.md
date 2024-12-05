# Test-Exam

## Overview
This utility is designed to process data from a text file, implement route-based calculations, and provide solutions for various routing problems through specialized services.

## Features
- Read and process data from a text file.
- Repository to fetch data based on input samples.
- Modular services, each tailored to specific requirements for better code organization and maintainability.

## Service Implementations
### 1. `CalculateForDistanceByRouteService`
This service is used for solving problems 1 to 5.  
- **Purpose**: Calculate the total distance for a given route.
- **Method**: 
  - `Get(string path)`: Accepts a route (e.g., `"A=>B=>C"`) and returns the total distance.
  - Private helper method `Find(string source, string destination, int currentDistance, string lookupPath)`:
    - Iterates through possible routes.
    - Tracks visited paths to prevent re-visitation.
    - Recursively calculates and returns the cumulative distance once the route is found.

---

### 2. `TripsBetweenTwoLocationService`
This service handles problems 6 and 7.  
- **Purpose**: Calculate the number of trips between two points based on specific conditions.  
- **Subclasses**:
  - `BaseGetTrip` (Abstract Class): 
    - Houses reusable properties and defines the `Get` method to be implemented by subclasses.
  - `HavingMaximumTrips`: Solves problem 6 (Maximum trips between two locations).
  - `HavingExactTrips`: Solves problem 7 (Exact number of trips between two locations).

---

### 3. `ShortestPathService`
This service is used for solving problems 8 and 9.  
- **Purpose**: Find the shortest path between two points.  
- **Implementation**:
  - Iteratively checks if the current cumulative distance is less than the previous shortest distance.
  - Updates the shortest distance until the final result is achieved.

---

### 4. `TripsBetweenTwoLocationService` (Revisited for Problem 10)
This service is reused but with a modified implementation for problem 10.  
- **Purpose**: Count trips between two points based on distance rather than the number of trips.
- **Implementation**:
  - Shares a similar recursive structure to the service used in problems 6 and 7 but tailored for distance-based conditions.

---

## Unit Testing
- Tests are divided into classes corresponding to each service.
- Each test class contains methods (Facts) specifically addressing the problem it solves.
- The input data can be customized by modifying `Input.text` to test different scenarios.


---
## Usage
1. Clone this repository.
2. Place your input data in the `Input.text` file.
3. Use the provided unit tests to verify functionality or test custom inputs.
