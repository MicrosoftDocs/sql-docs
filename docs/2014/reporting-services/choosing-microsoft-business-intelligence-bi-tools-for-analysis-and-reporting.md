## Get started with service principal

Different from the traditional use of a master user account, using the service principal (app-only token) requires a few different pieces to set up. Now to get started with the service principal (app-only token) you need to set up the right environment.

1. You need to [register an Azure AD application](register-app.md) to capture an Application ID and an Application secret to access your Power BI content.  When you use the [register app tool](https://dev.powerbi.com/apps), choose **Server-side web application** to go through the process of gathering an Application ID and an Application secret.


Manage multi-tenancy with Power BI embedded analytics


## Migrate to service principal

You can take steps to migrate to use service principal if you're currently using a master user account with Power BI or with your Power BI Embedded application.

1. You need to [register an Azure AD application](register-app.md) to capture an Application ID and an Application secret to access your Power BI content.  When you use the [register app tool](https://dev.powerbi.com/apps), choose **Server-side web application** to go through the process of gathering an Application ID and an Application secret.

2. Create [new workspaces](../service-create-the-new-workspaces.md) in the Power BI service to copy or move your Power BI artifacts.

3. [Copy or move Power BI artifacts into the new workspaces](https://powerbi.microsoft.com/pt-br/blog/duplicate-workspaces-using-the-power-bi-rest-apis-a-step-by-step-tutorial/). Currently there is no UI feature to move Power BI artifacts from one workspace to another. As such, you need to use APIs to accomplish this task.

4. Then with an admin master user account, sign in to Power BI and enable the service principal developer setting in the Power BI admin portal.

    ![Admin portal](media/embed-service-principal/admin-portal.png)

5. Create a [security group in Azure](https://docs.microsoft.com/azure/virtual-network/security-overview), and add the application you created to that security group.

6. Now log into Power BI with a Power BI Pro license to be able to access the workspaces you created, and add the security group you created to the workspace as an admin.

    ![Workspace admin](media/embed-service-principal/add-workspace-admin.png)
	
	
## Using PowerShell to create a service principal application

You can also use PowerShell to create a service principal application.  Below is a sample of how to create a service principal application using PowerShell.

1. Open PowerShell as an administrator

2. Install the Azure AD module

    ```powershell
    Install-Module -Name AzureAD
    ```
3. Then run the command *Set-ExecutionPolicy -ExecutionPolicy Bypass*

    ```powershell
    Set-ExecutionPolicy -ExecutionPolicy Bypass
    ```
4. Run PowerShell script

    ```powershell
    param (
    [string]$applicationName
    )

    # Login to Azure and be able to use the app cmdlets
    Connect-AzureAD
    Login-AzureRmAccount

    # Create a new AAD web application
    $App = New-AzureADApplication -DisplayName $applicationName -Homepage "https://localhost:44322" -ReplyUrls "https://localhost:44322"

    # Add service principal to the application (only for allowed users)
    New-AzureRmADServicePrincipal -ApplicationId $App.AppId
    ```

    > [!Note]
    > Once you exit out of the blade, it is not visible anymore so save it. However, if you forget to save it, then you can create a new one.

