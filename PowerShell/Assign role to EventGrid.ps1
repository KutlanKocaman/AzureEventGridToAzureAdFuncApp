Connect-AzureAD -TenantId "" # Insert your tenant ID
$webhookAppRegistrationObjectId = "" # Insert the Object ID of your app registration
$eventGridAppRoleId = "" # Insert the ID of your app registration role

$webhookSubscriberApp = Get-AzureADApplication -ObjectId $webhookAppRegistrationObjectId
$webhookSubscriberAppServicePrincipal = Get-AzureADServicePrincipal -Filter ("appId eq '" + $webhookSubscriberApp.AppId + "'")

$eventGridAppId = "4962773b-9cdb-44cf-a8bf-237846a00ab7" # Microsoft.EventGrid Application ID for Azure Public Cloud
$eventGridSP = Get-AzureADServicePrincipal -Filter ("appId eq '" + $eventGridAppId + "'")
New-AzureADServiceAppRoleAssignment -Id $eventGridAppRoleId -ResourceId $webhookSubscriberAppServicePrincipal.ObjectId -ObjectId $eventGridSP.ObjectId -PrincipalId $eventGridSP.ObjectId
