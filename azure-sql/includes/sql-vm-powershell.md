---
author: bluefooted
ms.author: pamela
ms.reviewer: vanto
ms.date: 09/22/2025
ms.service: azure-vm-sql-server
ms.topic: include
---

## Start your PowerShell session

Run the [**Connect-AzAccount**](/powershell/module/Az.Accounts/Connect-AzAccount) cmdlet and you're presented with a sign-in screen to enter your credentials. Use the same credentials that you use to sign in to the Azure portal.

```powershell
Connect-AzAccount
```

If you have multiple subscriptions, use the [**Set-AzContext**](/powershell/module/az.accounts/set-azcontext) cmdlet to select which subscription your PowerShell session should use. To see what subscription the current PowerShell session is using, run [**Get-AzContext**](/powershell/module/az.accounts/get-azcontext). To see all your subscriptions, run [**Get-AzSubscription**](/powershell/module/az.accounts/get-azsubscription).

```powershell
Set-AzContext -SubscriptionId 'aaaa0a0a-bb1b-cc2c-dd3d-eeeeee4e4e4e'
```
