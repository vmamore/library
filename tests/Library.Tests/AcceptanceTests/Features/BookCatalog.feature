Feature: Book Catalog

Background: 
    Given a Librarian user

Scenario: Validate book catalog
    When fetching book catalog
    Then should response be of success
    And contain these books