---
title: AD mode login fails - untrusted domain
titleSuffix: SQL Server Big Data Cluster
description: Fix behavior - clients fail to Authenticate when endpoints DNS entries are configures as CNAME pointing to an alias name.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 05/01/2020
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: troubleshooting
---

# Symptom: AD mode login fails - untrusted domain (Big Data Clusters)

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

On a SQL Server Big Data Cluster in Active Directory mode, a connection attempt may fail and the connection attempt returns the following error:

`Login failed. The login is from an untrusted domain and cannot be used with Integrated authentication.`

This can happens when you have configured DNS entries as CNAME pointing to an alias name of reverse proxy that distributes the traffic to Kubernetes nodes.

## Root cause

When the endpoints are configured with DNS entries with CNAME pointing to an alias name of reverse proxy that distributes the traffic to Kubernetes nodes:

- Kerberos authentication process looks for a service principal name (SPN) that matches the entry for CNAME; not the true SPN registered by BDC in active directory
- Authentication fails 

## Confirm root cause

After authentication fails, check the cache of Kerberos tickets. 

To check the cache of tickets, use `klist` command. 

Look for a ticket with an SPN matching the endpoint you tried to connect to.

The expected ticket is not there.

In this example, a master endpoint, `bdc-sql` DNS record is CNAME set to reverse proxy named `ServerReverseProxy`

```PowerShell
Resolve-DnsName bdc-sql
```

The following section shows the results from the previous command.

```
Name                           Type   TTL   Section    NameHost
----                           ----   ---   -------    --------
bdc-sql.mydomain.com           CNAME  3600  Answer     ReverseProxyServer.mydomain.com

Name       : ReverseProxyServer.mydomain.com
QueryType  : A
TTL        : 3600
Section    : Answer
IP4Address : 193.168.5.10
```

>[!NOTE]
>The following section references  [`tshark`](https://www.wireshark.org/docs/man-pages/tshark.html). `tshark` is a command line utility installed as part of [Wireshark](https://www.wireshark.org/docs/) network tracing utility).

To see the SPN requested from active directory, use `tshark`. The following command limits network tracing capture to Kerberos protocol communication and shows only `krb-error (30)` messages. These messages should contain failed SPN request messages.

```bash
tshark -Y "kerberos && kerberos.msg_type == 30" -T fields -e kerberos.error_code -e kerberos.SNameString
```

From a different command shell, try to connect to the master pod:

```bash
klist purge

sqlcmd -S bdc-sql.mydomain.com,31433 -E
```

See the following example output.

```bash
klist purge

Current LogonId is 0:0xf6b58
        Deleting all tickets:
        Ticket(s) purged!

sqlcmd -S bdc-sql.mydomain.com,31433 -E
sqlcmd: Error: Microsoft ODBC Driver 17 for SQL Server : Login failed. The login is from an untrusted domain and cannot be used with Integrated authentication.
```

Check the `tshark` output. 

```bash
Capturing on 'Ethernet 3'
25      krbtgt,RLAZURE.COM
7       MSSQLSvc,ReverseProxyServer.mydomain.com:31433
2 packets captured
```

Notice the client requests `SPN MSSQLSvc,ReverseProxyServer.mydomain.com:31433` which doesn't exist. The connection attempt eventually fails with error 7. Error 7 means `KRB5KDC_ERR_S_PRINCIPAL_UNKNOWN Server not found in Kerberos database`.

In the correct configuration, the client requests the SPN registered by BDC. In the example, the correct SPN would have been `MSSQLSvc,bdc-sql.mydomain.com:31433`.

>[!NOTE]
>Error 25 means `KDC_ERR_PREAUTH_REQUIRED` - additional pre-authentication required. It can safely be ignored. `KDC_ERR_PREAUTH_REQUIRED` is returned on the initial Kerberos AD request. By default, the Windows Kerberos Client is not including pre-authentication information in this first request.

To see the list of SPN registered by BDC for master endpoint, run `setspn -L mssql-master`. 

See the following example output:

```bash
Registered ServicePrincipalNames for CN=mssql-master,OU=bdc,DC=mydomain,DC=com:
        MSSQLSvc/bdc-sqlread.mydomain.com:31436
        MSSQLSvc/-sqlread:31436
        MSSQLSvc/bdc-sqlread.mydomain.com
        MSSQLSvc/bdc-sqlread
        MSSQLSvc/bdc-sql.mydomain.com:31433
        MSSQLSvc/bdc-sql:31433
        MSSQLSvc/bdc-sql.mydomain.com
        MSSQLSvc/bdc-sql
        MSSQLSvc/master-p-svc.mydomain.com:1533
        MSSQLSvc/master-p-svc:1533
        MSSQLSvc/master-p-svc.mydomain.com:1433
        MSSQLSvc/master-p-svc:1433
        MSSQLSvc/master-p-svc.mydomain.com
        MSSQLSvc/master-p-svc
        MSSQLSvc/master-svc.mydomain.com:1533
        MSSQLSvc/master-svc:1533
        MSSQLSvc/master-svc.mydomain.com:1433
        MSSQLSvc/master-svc:1433
        MSSQLSvc/master-svc.mydomain.com
        MSSQLSvc/master-svc
```

In the results above the reverse proxy address should not be registered.

## Resolve

This section shows two ways to resolve the issue. After making the appropriate changes, run `ipconfig -flushdns` and `klist purge` in your client. Then attempt to connect again.

### Option 1

Remove the CNAME record for each BDC endpoint in DNS and replace with multiple `A` records that points to each Kubernetes node or each Kubernetes master if you have more than one master.

>[!TIP]
>The script described below uses PowerShell. See [Installing PowerShell on Linux](/powershell/scripting/install/installing-powershell-core-on-linux) for more information.

You can use the following PowerShell Script to update DNS endpoints records. Run the script from any computer connected to the same domain:

```powershell
#Specify the DNS server, example contoso.local
$Domain_DNS_name=mydomain.com'

#DNS records for bdc endpoints
$Controller_DNS_name = 'bdc-control'
$Managment_proxy_DNS_name= 'bdc-proxy'
$Master_Primary_DNS_name = 'bdc-sql'
$Master_Secondary_DNS_name = 'bdc-sqlread'
$Gateway_DNS_name = 'bdc-gateway'
$AppProxy_DNS_name = 'bdc-appproxy'

#Performing Endpoint DNS records Checks..

#Build array of endpoints 
$BdcEndpointsDns = New-Object System.Collections.ArrayList

[void]$BdcEndpointsDns.Add($Controller_DNS_name)
[void]$BdcEndpointsDns.Add($Managment_proxy_DNS_name)
[void]$BdcEndpointsDns.Add($Master_Primary_DNS_name)
[void]$BdcEndpointsDns.Add($Master_Secondary_DNS_name)
[void]$BdcEndpointsDns.Add($Gateway_DNS_name)
[void]$BdcEndpointsDns.Add($AppProxy_DNS_name)

#Build arrary for results 
$BdcEndpointsDns_Result = New-Object System.Collections.ArrayList

foreach ($DnsName in $BdcEndpointsDns) {
    try {
        $endpoint_DNS_record = Resolve-DnsName $DnsName -Type A -Server $Domain_DNS_IP_address -ErrorAction Stop 
        foreach ($ip in $endpoint_DNS_record.IPAddress) {
            [void]$BdcEndpointsDns_Result.Add("OK - $DnsName is an A record with an IP $ip")
        }
    }
    catch {
        [void]$BdcEndpointsDns_Result.Add("MisConfiguration - $DnsName is not an A record or does not exists")
    }  
}

#show the results 
$BdcEndpointsDns_Result
```

### Option 2

Alternatively, it's possible to work around the issue by modifying the CNAME to point to the IP address of the reverse proxy rather than the name of the reverse proxy.

## Confirm Resolution

After resoling the fix with one of the options above, confirm the fix by connecting to Big Data Cluster with active directory.

## Next steps

[Verify reverse DNS entry (PTR record) for domain controller](active-directory-deploy.md#verify-reverse-dns-entry-for-domain-controller).
