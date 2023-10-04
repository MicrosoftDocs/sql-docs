Secure, passwordless connections to Azure SQL Database require certain database configurations. Verify the following settings on your [logical server in Azure](../database/logical-servers.md) to properly connect to Azure SQL Database in both local and hosted environments:

1) For local development connections, make sure your logical server is configured to allow your local machine IP address and other Azure services to connect:

    * Navigate to the **Networking** page of your server.
    * Toggle the **Selected networks** radio button to show additional configuration options.
    * Select **Add your client IPv4 address(xx.xx.xx.xx)** to add a firewall rule that will enable connections from your local machine IPv4 address. Alternatively, you can also select **+ Add a firewall rule** to enter a specific IP address of your choice.
    * Make sure the **Allow Azure services and resources to access this server** checkbox is selected.

        :::image type="content" source="../database/media/passwordless-connections/configure-firewall-small.png" lightbox="../database/media/passwordless-connections/configure-firewall.png" alt-text="A screenshot showing how to configure firewall rules.":::

        > [!WARNING]
        > Enabling the **Allow Azure services and resources to access this server** setting is not a recommended security practice for production scenarios. Real applications should implement more secure approaches, such as stronger firewall restrictions or virtual network configurations.
        >
        > You can read more about database security configurations on the following resources:
        >
        > - [Configure Azure SQL Database firewall rules](/azure/azure-sql/database/firewall-configure).
        > - [Configure a virtual network with private endpoints](/azure/private-link/tutorial-private-endpoint-sql-portal).

1) The server must also have Azure AD authentication enabled with an Azure Active Directory admin account assigned. For local development connections, the Azure Active Directory admin account should be an account you can also log into Visual Studio or the Azure CLI with locally. You can verify whether your server has Azure AD authentication enabled on the **Azure Active Directory** page.

    :::image type="content" source="../database/media/passwordless-connections/enable-active-directory-small.png" lightbox="../database/media/passwordless-connections/enable-active-directory.png" alt-text="A screenshot showing how to enable Active Directory authentication.":::

1) If you're using a personal Azure account, make sure you have [Azure Active Directory setup and configured for Azure SQL Database](../database/authentication-aad-configure.md) in order to assign your account as a server admin. If you're using a corporate account, Azure Active Directory will most likely already be configured for you.