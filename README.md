# Paylocity BackEnd Challenge

## Problem statement with requirements
Implement a backend solution where a user, an employee, has their paychecks calculated for a year
based on the following requirements:
- view employees and their dependents
- an employee may only have 1 spouse or domestic partner (not both), this does not need to be implemented explicitly,
  but can serve to limit your test cases
- an employee may have an unlimited number of children
- calculate and view a paycheck for an employee given the following rules:
  - 26 paychecks per year with deductions spread as evenly as possible on each paycheck
  - employees have a base cost of $1,000 per month (for benefits)
  - each dependent represents an additional $600 cost per month (for benefits)
  - employees that make more than $80,000 per year will incur an additional 2% of their yearly salary in benefits costs
  - dependents that are over 50 years old will incur an additional $200 per month

## Tasks
Tasks should be tracked in some issue tracking system. I will use this section for simplicity.
- [X] Check the provided code base and create tasks
- [X] Update TargetFramework to latest supported LTS
- [X] Use http for development
- [X] Add persistence
- [X] Implement dependent and employee services
- [X] Implement dependents controller methods
- [X] Implement the paycheck calculation method
- [X] Implement employees controller methods
- [X] Check the codebase test coverage and add missing tests
- [X] Check all requirements are met and covered by passing test cases
- [X] Review the codebase
- [X] Refactor integration tests to use WebApplicationFactory
- [X] Restructure the project for future enhancements
- [X] Make DTOs immutable
- [X] Add an integration test for the employees controller GetPayStub method
- [X] List possible enhancements based on how the project evolves

## Possible enhancements based on how the project evolves
- Update naming conventions to better align with the business
- Use standardized API problem details (https://tools.ietf.org/html/rfc7807)
- Fluent assertions for better readability
- Add clock abstraction to test some time-based calculations
- Refactor employee and services to separated use-cases
- Support multiple currencies
- Introduce repository pattern to separate data access
- Switch to production-ready database provider