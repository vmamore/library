$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$headers.Add("Content-Type", "application/x-www-form-urlencoded")

$body = "client_id=admin-cli&grant_type=password&username=admin&password=secret"

$tokenResponse = Invoke-RestMethod 'http://localhost:8080/auth/realms/master/protocol/openid-connect/token' -Method 'POST' -Headers $headers -Body $body

$jsonResponse = $tokenResponse | ConvertTo-Json | ConvertFrom-Json
$jsonResponse.access_token

$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$headers.Add("Authorization", "Bearer " + $jsonResponse.access_token)
$headers.Add("Content-Type", "application/json")

$locatorBody = "{
`n   `"firstName`":`"Bob`",
`n   `"lastName`":`"Aerso`",
`n   `"email`":`"bob.aerso@gmail.com`",
`n   `"enabled`":`"true`",
`n   `"emailVerified`":true,
`n   `"username`":`"bob.aerso`",
`n   `"attributes`":{
`n      `"library_id`":`"e4eafac2-0e6d-463d-b76a-cab2a9ff2f7c`"
`n   },
`n   `"groups`":[
`n      `"locators`"
`n   ],
`n   `"credentials`":[
`n      {
`n         `"type`":`"password`",
`n         `"value`":`"@test123`",
`n         `"temporary`":false
`n      }
`n   ]
`n}"

try { 
    $locatorResponse = Invoke-WebRequest 'http://localhost:8080/auth/admin/realms/library/users' -Method 'POST' -Headers $headers -Body $locatorBody

    if ( $locatorResponse.StatusCode -eq 201) {
        Write-Output "Locator created with success."
    } else {
        Write-Output "Error when creating locator."
    }
} catch {
    Write-Output "Error when creating locator."
}

$librarianBody = "{
    `n   `"firstName`":`"Amanda`",
    `n   `"lastName`":`"Lacerda`",
    `n   `"email`":`"amanda.lacerda@gmail.com`",
    `n   `"enabled`":`"true`",
    `n   `"emailVerified`":true,
    `n   `"username`":`"amanda.lacerda`",
    `n   `"attributes`":{
    `n      `"library_id`":`"e2487e01-5cf7-43bc-b186-a64312e4bb49`"
    `n   },
    `n   `"groups`":[
    `n      `"librarians`"
    `n   ],
    `n   `"credentials`":[
    `n      {
    `n         `"type`":`"password`",
    `n         `"value`":`"@test123`",
    `n         `"temporary`":false
    `n      }
    `n   ]
    `n}"
    
    try { 
        $librarianResponse = Invoke-WebRequest 'http://localhost:8080/auth/admin/realms/library/users' -Method 'POST' -Headers $headers -Body $librarianBody
    
        if ( $librarianResponse.StatusCode -eq 201) {
            Write-Output "Librarian created with success."
        } else {
            Write-Output "Error when creating librarian."
        }
    } catch {
        Write-Output "Error when creating librarian."
    }

