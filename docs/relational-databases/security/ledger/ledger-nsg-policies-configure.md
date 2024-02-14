---
title: Configure Azure SQL Managed Instance NSG rules to work with Azure Confidential Ledger
description: "Learn how to configure Azure SQL Managed Instance NSG rules to work with Azure Confidential Ledger." 
ms.service: sql-managed-instance
author: Pietervanhove
ms.author: pivanho
ms.subservice: security
ms.topic: how-to
ms.reviewer: vanto
ms.date: 02/07/2024
---

# Configure Azure SQL Managed Instance NSG rules to work with Azure Confidential Ledger

[!INCLUDE [Azure SQL Managed Instance](../../../includes/applies-to-version/asmi.md)]

After you have [enabled Azure Confidential Ledger](ledger-how-to-enable-automatic-digest-storage.md) as your digest location on your [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview), you'll need to manually configure the virtual network rules of your Azure SQL Managed Instance to communicate with Azure Confidential Ledger.

In this article, you learn how to:

> [!div class="checklist"]
>
> - Configure your SQL Managed Instance network security group (NSG) and routing table rules to allow traffic to Azure Confidential Ledger.

## Permissions

Due to the sensitivity of data in a managed instance, the configuration to enable Azure SQL Managed Instance public endpoint requires a two-step process. This security measure adheres to separation of duties (SoD):

- The SQL Managed Instance administrator needs to enable the public endpoint on the SQL Managed Instance. The SQL Managed Instance administrator can be found on the **Overview** page for your SQL Managed Instance resource.
- A network administrator needs to allow traffic to the SQL Managed Instance using an NSG. For more information, see [network security group permissions](/azure/virtual-network/manage-network-security-group#permissions).

## Enable outbound NSG rules for Azure Confidential Ledger

We'll need to capture the IP addresses of the Azure Confidential Ledger and add them to the outbound NSG rules and route table of your SQL Managed Instance.

### Obtain ledger endpoint and identity service endpoint IP addresses

In your provisioned Azure Confidential Ledger **Overview** page in the Azure portal, capture the **Ledger Endpoint** hostname. Acquire the IP address of your Azure Confidential Ledger instance by using `ping` or a similar network tool.

```powershell
ping -a <ledgername>.confidential-ledger.azure.com
```

```output
PING <ledgername>.confidential-ledger.azure.com (1.123.123.123) 56(84) bytes of data.
64 bytes from 1.123.123.123 (1.123.123.123): icmp_seq=1 ttl=105 time=78.7 ms
```

Similarly, perform the procedure for the Azure Confidential Ledger instance `Identity Service Endpoint`.

```powershell
ping identity.confidential-ledger.core.azure.com
```

```output
PING part-0042.t-0009.t-msedge.net (13.107.246.70) 56(84) bytes of data.
64 bytes from 13.107.246.70 (13.107.246.70): icmp_seq=1 ttl=52 time=14.9 ms
```

### Add IP Addresses to the outbound NSG rules

These two IP addresses should be added to the outbound NSG rules of your SQL Managed Instance.

1. In the Azure portal, go to the **Network security group** of your SQL Managed Instance. The **Network security group** is a separate resource in the **Resource group** of your SQL Managed Instance.
1. Go to the **Outbound security rules** menu.
1. Add the two IP addresses obtained in the previous section as a new outbound rule:

   Select the **Outbound security rules** tab, and **Add** a rule that has higher priority than the **deny_all_inbound** rule with the following settings: </br> </br>

    |Setting  |Suggested value  |Description  |
    |---------|---------|---------|
    |**Source**     |Any IP address or Service tag         |<ul><li>For Azure services like Power BI, select the Azure Cloud Service Tag</li> <li>For your computer or Azure virtual machine, use NAT IP address</li></ul> |
    |**Source port ranges**     |* |Leave this as * (any) as source ports are typically dynamically allocated and as such, unpredictable |
    |**Destination**     |<1.123.123.123>, <13.107.246.70>         |Add the IP addresses obtained in the previous section for Azure Confidential Ledger |
    |**Destination port ranges**     |3342         |Scope destination port to 3342, which is the managed instance public TDS endpoint |
    |**Service**     |HTTPS         |SQL Managed Instance will communicate with ledger over HTTPS |
    |**Action**     |Allow         |Allow outbound traffic from managed instance to ledger |
    |**Priority**     |1500         |Make sure this rule is higher priority than the **deny_all_inbound** rule |

    :::image type="content" source="./media/ledger/ledger-nsg-outbound-rules.png" alt-text="Screenshot of NSG outbound rules to enable SQL to communicate with the ledger.":::

### Add IP addresses to the route table

The two Azure Confidential Ledger IP addresses should also be added to the Route table:

1. In the Azure portal, go to the **Route table** of your SQL Managed Instance. The **Route table** is a separate resource in the **Resource group** of your SQL Managed Instance.
1. Go to the **Routes** menu under **Settings**.
1. Add the two IP addresses obtained in the previous section as new routes:

    |Setting  |Suggested value  |Description  |
    |---------|---------|---------|
    |**Route name**     | Use a preferred name       | Name that you want to use for this route |
    |**Destination type**     |IP Addresses       | Use the drop-down menu and select **IP Addresses** |
    |**Destination IP addresses/CIDR ranges**     | 1.123.123.123/32 | In this example, we use `1.123.123.123/32`. Create another route to add the identity service endpoint, which is `13.107.246.70/32` in this example |
    |**Next hop type**     |Internet         | |

    :::image type="content" source="./media/ledger/ledger-routing-rule.png" alt-text="Screenshot of adding a route for the VNET to the ledger.":::

## Verify that the routing is properly configured

You can confirm that your SQL Managed Instance is now able to communicate with the Azure Confidential Ledger by running a [database verification](./ledger-database-verification.md). The query should report that `Ledger verification succeeded`.

## Related content

- [Digest management](ledger-digest-management.md)