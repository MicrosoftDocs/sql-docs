---
title: "Connection Properties Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "ConnectionTimeout property [ADO], VJ++ example"
  - "ConnectionString property [ADO], VJ++ example"
  - "State property [ADO], VJ++ example"
ms.assetid: 4c1e61ed-6569-44a9-b0c8-75b820a64cb6
caps.latest.revision: 12
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# ConnectionString, ConnectionTimeout, and State Properties Example (VJ++)
This example demonstrates different ways of using the [ConnectionString](../../../ado/reference/ado-api/connectionstring-property-ado.md) property to open a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object. It also uses the [ConnectionTimeout](../../../ado/reference/ado-api/connectiontimeout-property-ado.md) property to set a connection time-out period, and the [State](../../../ado/reference/ado-api/state-property-ado.md) property to check the state of the connections. The GetState function is required for this procedure to run.  
  
> [!NOTE]
>  If you are connecting to a data source provider that supports Windows authentication, you should specify **Trusted_Connection=yes** or **Integrated Security = SSPI** instead of user ID and password information in the connection string.  
  
```  
// BeginConnectionStringJ  
import com.ms.wfc.data.*;  
import java.io.* ;  
  
public class ConnectionStringX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      ConnectionStringX();  
      System.exit(0);  
   }  
  
   // ConnectionStringX function  
  
   static void ConnectionStringX()  
   {  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
      Connection cnConn2 = null;  
      Connection cnConn3 = null;  
      Connection cnConn4 = null;  
  
      //Declarations.  
  
      BufferedReader in =  
         new BufferedReader (new InputStreamReader(System.in));  
      String line = null;  
      String strTemp;  
  
      try  
      {  
         // Open a connection using OLE DB syntax.  
         cnConn1 = new Connection();  
         cnConn1.setConnectionString(  
            "Provider='sqloledb';Data Source='MySqlServer';" +  
            "Initial Catalog='Pubs';Integrated Security='SSPI';");  
         cnConn1.setCommandTimeout(30);  
         cnConn1.open();  
         strTemp = getState(cnConn1.getState());  
         System.out.println("CnConn1 state: " + strTemp);  
  
         // Open a connection using a DSN and ODBC tags.  
         // It is assumed that you have create DSN 'Pubs' with a user name as   
         // 'MyUserId' and password as 'MyPassword'.  
         cnConn2 = new Connection();  
         cnConn2.setConnectionString("DSN='Pubs';UID='MyUserId';PWD='MyPassword';");  
         cnConn2.open();  
         strTemp = getState(cnConn2.getState());  
         System.out.println("CnConn2 state: " + strTemp);  
  
         // Open a connection using a DSN and OLE DB tags.  
         cnConn3 = new Connection();  
         cnConn3.setConnectionString  
            ("Data Source='Pubs';");  
         cnConn3.open();  
         strTemp = getState(cnConn3.getState());  
         System.out.println("CnConn3 state: " + strTemp);  
  
         // Open a connection using a DSN and individual  
         // arguments instead of a connection string.  
         // It is assumed that you have create DSN 'Pubs' with a user name as   
         // 'MyUserId' and password as 'MyPassword'.  
         cnConn4 = new Connection();  
         cnConn4.open("Pubs", "MyUserId", "MyPassword");  
         strTemp = getState(cnConn4.getState());  
         System.out.println("CnConn4 state: " + strTemp);  
  
         System.out.println("\n\nPress <Enter> to continue..");  
         in.readLine();  
  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         System.out.println("Exception: " + ae.getMessage());  
  
      }  
  
      // System read requires this catch.  
      catch( java.io.IOException je)  
      {  
         PrintIOError(je);  
      }  
  
      finally  
      {  
         // Cleanup objects before exit.     
         if (cnConn1 != null)  
            if (cnConn1.getState() == 1)  
               cnConn1.close();    
         if (cnConn2 != null)  
            if (cnConn2.getState() == 1)  
               cnConn2.close();    
         if (cnConn3 != null)  
            if (cnConn3.getState() == 1)  
               cnConn3.close();    
         if (cnConn4 != null)  
            if (cnConn4.getState() == 1)  
               cnConn4.close();  
      }  
  
   }  
  
   // getState Function  
  
   static String getState(int intState)  
   {  
      // Returns current state of the connection object.  
      String strState=null;  
  
      switch(intState)  
      {  
      case AdoEnums.ObjectState.CLOSED :  
         strState = new String("adStateClosed");  
         break;  
      case AdoEnums.ObjectState.OPEN  :  
         strState = new String("adStateOpen");  
         break;  
      default :  
         break;  
      }  
      return strState;  
   }  
  
   // PrintIOError Function  
  
   static void PrintIOError( java.io.IOException je)  
   {  
      System.out.println("Error \n");  
      System.out.println("\tSource = " + je.getClass() + "\n");  
      System.out.println("\tDescription = " + je.getMessage() + "\n");  
   }  
}  
  
// EndConnectionStringJ  
```  
  
## See Also  
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)   
 [ConnectionString Property (ADO)](../../../ado/reference/ado-api/connectionstring-property-ado.md)   
 [ConnectionTimeout Property (ADO)](../../../ado/reference/ado-api/connectiontimeout-property-ado.md)   
 [State Property (ADO)](../../../ado/reference/ado-api/state-property-ado.md)
