Feature: Validating the login button	
  We want to validate the login button

@buttob_exist
  Scenario: Validate Login Button Exists
    Given I am on the Login screen
    Then I should see a button named "Active Directory"

@button_color
 Scenario: Validate Login Button color is #3b5998
   Given I am on the Login screen
   Then I should see a button named "Active Directory"
   And The button named "Active Directory" should have the color Hex code "#3b5998"

