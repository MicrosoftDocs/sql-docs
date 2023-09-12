---
title: "Azure connectivity"
description: Explains the Azure connection options available in Azure Data Studio.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 06/06/2023
ms.service: azure-data-studio
ms.topic: "overview"
---
# Azure Data Studio - Azure connectivity

Azure Data Studio uses the Microsoft Authentication Library (MSAL) by default to acquire an access token from Azure Active Directory. The settings that apply to Azure authentication are discussed, along with commonly observed issues and their solutions.

## Azure: Authentication library

This setting controls the authentication library used by Azure Data Studio when adding an Azure account. Microsoft Authentication Library (MSAL) offers authentication and authorization services using standard-compliant implementations of OAuth 2.0 and OpenID Connect (OIDC) 1.0. Read more about [Microsoft Authentication Library (MSAL)](/azure/active-directory/develop/msal-overview).

`Settings.json`

```json
"azure.authenticationLibrary": "MSAL"
```

:::image type="content" source="media/azure-connectivity/authentication-library.png" alt-text="Screenshot of Azure authentication library.":::

**ADAL (Active Directory Authentication Library)** is now deprecated, but is supported by Azure Data Studio as a fallback for any potential authentication issues with MSAL.

> [!NOTE]  
> Switching the authentication library requires reloading Azure Data Studio. Existing Azure accounts are marked stale and users must re-authenticate with the selected library.

## Azure authentication method

Azure Data Studio supports multi-factor authentication with Azure accounts using the following modes:

- Using Code Grant authentication (enabled by default)
- Using Device Code authentication

### Accounts > Azure > Auth: Code Grant

`Settings.json`

```json
"accounts.azure.auth.codeGrant": true
```

:::image type="content" source="media/azure-connectivity/code-grant.png" alt-text="Screenshot of Azure authentication Code Grant option.":::

When **Code Grant method** is checked, users are prompted to authenticate with browser based authentication. This option is enabled by default.

### Accounts > Azure > Auth: Device Code

`Settings.json`

```json
"accounts.azure.auth.deviceCode": true
```

:::image type="content" source="media/azure-connectivity/device-code.png" alt-text="Screenshot of Azure authentication Device Code option.":::

When **Device Code method** is enabled, users are provided with a code and a URL to enter which can then be used to sign in.

When both options are checked, users are prompted to select one of the two authentication modes when adding an Azure account.

## Azure account configuration

Azure Data Studio supports Azure Active Directory (Azure AD) authentication with National clouds. **Azure Public Cloud** is enabled by default, but users can enable other national clouds as needed:

`Settings.json`

```json
"accounts.azure.cloud.enableChinaCloud": false,
"accounts.azure.cloud.enablePublicCloud": true,
"accounts.azure.cloud.enableUsGovCloud": false
```

:::image type="content" source="media/azure-connectivity/national-clouds.png" alt-text="Screenshot of Azure authentication National Clouds.":::

## Azure resource configuration

These settings apply filters on Azure resources and tenants.

- Resource Config filter: Applies inclusion filter to resources that should be displayed.
- Tenant Config filter: Applies exclusion filter to tenants that should be ignored.

`Settings.json`

```json
"azure.resource.config.filter": [],
"azure.tenant.config.filter": [
    "313b5f9e-9b92-414c-8d87-a317e42d0222"
]
```

:::image type="content" source="media/azure-connectivity/resource-config.png" alt-text="Screenshot of Azure authentication resource configuration options.":::

## Proxy setup for Azure authentication

If using Azure Data Studio (ADS) behind a proxy, users must specify proxy settings for ADS to communicate with external endpoints. There are two ways to provide proxy settings for ADS to use:

- Setting proxy configuration in the Azure Data Studio (**Settings > Http: Proxy Settings**), or
- Setting environment variables for proxy configuration.

Azure Data Studio settings take precedence over environment variables.

### Azure Data Studio proxy settings

The following settings are available in Azure Data Studio:

`Settings.json`

```json
"http.proxy": "https://userName@fqdn:yourPassword@yourProxyURL.com:8080",
"http.proxyStrictSSL": true,
"http.proxyAuthorization": "",
"http.proxySupport" : "override"
```

:::image type="content" source="media/azure-connectivity/proxy-settings.png" alt-text="Screenshot of Azure authentication proxy settings.":::

### Supported environment variables for proxy

- `'HTTP_PROXY': 'http://userName@fqdn:yourPassword@yourProxyURL.com:8080'`
- `'HTTPS_PROXY': 'https://userName@fqdn:yourPassword@yourProxyURL.com:8080'`

### Allowlist URLs

In a proxy environment, user applications may need to allow specific domains used by Azure Data Studio. Hostnames through which you may need or want to allow communication are:

**Azure Public**

- https://management.azure.com
- https://login.microsoftonline.com/

**Azure (US Government)**

- https://management.core.usgovcloudapi.net/
- https://login.microsoftonline.us/

**Azure operated by 21Vianet**

- https://management.core.chinacloudapi.cn/
- https://login.partner.microsoftonline.cn/

The URLs to allow can sometimes vary on a case-by-case basis. In order to verify you aren't blocking any URLs from going through, go to **Help > Toggle Developer Tools** and select the **Network** tab. Any URLs that are blocked are listed, and you may need to allow those URLs to successfully add your account.

## Common authentication issues

Possible issues and solutions when adding an Azure account are discussed.

### Issue: SSL Error on localhost page (ERR_SSL_PROTOCOL_ERROR)

Users may see an SSL error when signing in to their account. This flow opens an external web page to `localhost`, typically prompting users to sign in via the standard Microsoft authentication prompts. The URL for this page looks similar to `http://localhost:50055/signin?nonce=...`

Some browsers may be set up to automatically redirect all `http` links to `https`, which breaks this process as the local server serving the web page doesn't support https. If the link in the address bar starts with `https`, then you get an SSL error and the page can't load. In that case, the workarounds listed here may address the issue.

**Change URL to http**

First, manually change the URL from `https://` to `http://`. The browser may change it back to https, in which case it's necessary to try another option.

**Disable HSTS (HTTP Strict Transport Security)**

For Edge/Chrome browsers, you can disable [HSTS](https://www.chromium.org/hsts/) for localhost.

1. Open Edge/Chrome, and in the address bar, type `edge://net-internals/#hsts` (or `chrome://net-internals/#hsts` for Chrome).
1. Scroll to the bottom of the page and in the `Delete domain security policies` section, enter `localhost` and press `Delete`.

Once that is complete, you should be able to sign in, and not have the browser redirect your `localhost` links automatically to `https`.

### Issue: Unable to add account behind a proxy

If the user application is running in an environment behind a proxy, user authentication may not complete, and these steps can be used to resolve the issue.

1. Recheck environment variables and **http.proxy** settings in ADS. If the proxy requires user authentication, providing a username/password in **http.proxy** URL can resolve authentication issues; otherwise, ADS can't read logged-in user credentials. Alternatively, running ADS as a different user can be attempted, as it may help resolve Authentication issues with Proxy. However, the latter only works for some scenarios.

1. The URLs to allow can vary on a case-by-case basis. To verify you aren't blocking any URLs from going through, go to **Help > Toggle Developer Tools** and select the **Network** tab. Here you see any URLs that are getting blocked that you may need to allow to add your account successfully.

1. Uncheck **Http: Proxy Strict SSL**. It's possible that proxy certificate isn't verifiable against the list of trusted CAs. Disabling Strict SSL can rule out the proxy certificate as an issue.

**To conclude:**

As a cross-platform application, ADS proxy resolution fetches the proxy from either setting within the application, or through environment variables. The aim is to avoid interaction with system settings, which can vary significantly on different operating systems.

### Issue: Azure Core extension is disabled

Azure Core extension is `@builtin` extension in Azure Data Studio, please ensure it's not disabled or uninstalled accidentally. This extension is required to be able to authenticate Azure accounts and connect to resources with Azure MFA authentication.

:::image type="content" source="media/azure-connectivity/azure-connectivity-azure-core-extension.png" alt-text="Screenshot of built-in Azure Core extension.":::

### Issue: System CA certificates are expired

Azure Data Studio's default behavior includes validating system's root CA certificates when making REST API calls using HTTPS Protocol. This is controlled by the below setting that is enabled by default:

:::image type="content" source="media/azure-connectivity/azure-connectivity-system-certificates.png" alt-text="Screenshot of system certificates setting.":::

```
"http.systemCertificates": true
```

If a system's Root CA certificate is expired, authentication requests to Azure Active Directory will fail and an error like below would be captured in 'Azure Account' logs:

`error: certificate is expired`

To mitigate this error, you should remove any expired Root CA Certificates or disable the setting to not validate system certificates.

## Capture logs for Azure authentication

Azure Data Studio captures Error events for Azure account activity by default. To enable more detailed traces, users can modify these settings:

### Azure: Logging level

This setting configures the logging level for information from Azure core that can be captured in Azure Data Studio. Change it to *Verbose* or *All* to capture detailed logs that can be useful to diagnose authentication failures. For more information, see [Azure Data Studio logs and location](troubleshooting.md#azure-data-studio-logs-and-location) to learn how to capture logging information.

`Settings.json`

```json
"azure.loggingLevel": "Verbose"
```

:::image type="content" source="media/azure-connectivity/logging-level.png" alt-text="Screenshot of Azure authentication logging Level configuration.":::

### Azure: PII logging

Users can enable PII (Personally Identifiable Information) logging for local testing and debugging purposes. This setting enables more thorough logging of the authentication process, but may contain sensitive information such as access tokens or user IDs when authenticating with Azure. Because this logging captures sensitive information, it's recommended to:

- Not share these logs with anyone else, especially when adding logs to GitHub issues
- Disable the setting once the necessary information has been collected
- Delete the log files once the setting has been disabled

`Settings.json`

```json
"azure.piiLogging": true
```

:::image type="content" source="media/azure-connectivity/pii-logging.png" alt-text="Screenshot of Azure authentication PII logging option.":::

## Azure: No system keychain

This setting disables system keychain integration to prevent repeated keychain access prompts on macOS. User Credentials are alternatively stored in a flat file in the user's home directory.

`Settings.json`

```json
"azure.noSystemKeychain": true
```

:::image type="content" source="media/azure-connectivity/keychain.png" alt-text="Screenshot of Azure authentication keychain configuration.":::

## Clear Azure account token cache

Azure Data Studio maintains a cache of access tokens to prevent the throttling of token requests to Azure Active Directory (Azure AD). It's possible that Azure Data Studio's token cache may be outdated, which requires cleaning up expired access tokens from the application cache.

Execute this command from **Command Palette (Ctrl/CMD + Shift + P)** to clear access tokens for linked Azure accounts:

`Azure Accounts: Clear Azure Account Token Cache (accounts.clearTokenCache)`

## Clear all saved Azure accounts

Execute this command from **Command Palette (Ctrl/CMD + Shift + P)** to remove all linked Azure accounts from Azure Data Studio:

**Clear all saved accounts (clearSavedAccounts)**