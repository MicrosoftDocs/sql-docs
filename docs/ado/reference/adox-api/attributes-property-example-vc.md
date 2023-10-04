---
title: "Attributes Property Example (VC++)"
description: "Attributes Property Example (VC++)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Attributes property [ADOX], VC++ example"
dev_langs:
  - "C++"
---
# Attributes Property Example (VC++)
This example demonstrates the [Attributes](./attributes-property-adox.md) property of a [Column](./column-object-adox.md). Setting it to **adColNullable** allows the user to set the value of a [Recordset](../ado-api/recordset-object-ado.md) [Field](../ado-api/field-object.md) to an empty string. In this situation, the user can distinguish between a record where data is not known and a record where the data does not apply.  
  
```  
// Attributes_Property_Sample.cpp  
// compile with: /EHsc  
#import "msado15.dll" rename("EOF", "EndOfFile")  
#import "msadox.dll" no_namespace  
  
#include "iostream"  
#include "icrsint.h"  
using namespace std;  
  
// class extracts LastName, FirstName, FaxPhone from Employees table  
class CEmployeeRs : public CADORecordBinding {  
   BEGIN_ADO_BINDING(CEmployeeRs)  
  
   // Column LastName is the 2nd field in the table  
   ADO_VARIABLE_LENGTH_ENTRY2(2,   
                              adVarChar,   
                              m_szemp_LastName,   
                              sizeof(m_szemp_LastName),   
                              lemp_LastNameStatus,   
                              TRUE)  
  
   // Column FirstName is the 17th field in the table  
   ADO_VARIABLE_LENGTH_ENTRY2(17,   
                              adVarChar,   
                              m_szemp_FirstName,   
                              sizeof(m_szemp_FirstName),   
                              lemp_FirstNameStatus,   
                              TRUE)  
  
   // Column FaxPhone is the 18th field in the table  
   ADO_VARIABLE_LENGTH_ENTRY2(18,   
                              adVarChar,   
                              m_szemp_Faxphone,   
                              sizeof(m_szemp_Faxphone),   
                              lemp_FaxphoneStatus,   
                              TRUE)  
  
END_ADO_BINDING()  
  
public:  
   CHAR m_szemp_LastName[21];  
   ULONG lemp_LastNameStatus;  
   CHAR m_szemp_FirstName[11];  
   ULONG lemp_FirstNameStatus;  
   CHAR m_szemp_Faxphone[25];  
   ULONG lemp_FaxphoneStatus;  
};  
  
inline void TESTHR(HRESULT x) { if FAILED(x) _com_issue_error(x); };  
  
int main() {  
   if ( FAILED( ::CoInitialize(NULL) ) )  
      return -1;  
  
   HRESULT hr = S_OK;  
   char * token;  
  
   // Define ADOX object pointers, initialize pointers.  These are in ADODB namespace.  
   _CatalogPtr m_pCatalog = NULL;  
   _ColumnPtr m_pColumn = NULL;  
   _TablePtr m_pTable = NULL;  
  
   // Define ADODB object pointers  
   ADODB::_ConnectionPtr m_pCnn = NULL;  
   ADODB::_RecordsetPtr m_pRstEmployees = NULL;  
  
   IADORecordBinding *picRs = NULL;   // Interface Pointer Declared  
   CEmployeeRs emprs;   // C++ Class Object  
  
   // Define string variables.  
   _bstr_t strcnn("Provider='Microsoft.JET.OLEDB.4.0';Data Source= 'c:\\Northwind.mdb';");  
  
   try {  
      // Connect the catalog.  
      TESTHR(hr = m_pCnn.CreateInstance(__uuidof (ADODB::Connection)));  
      TESTHR(hr = m_pCatalog.CreateInstance(__uuidof (Catalog)));  
      TESTHR(hr = m_pColumn.CreateInstance(__uuidof(Column)));  
      TESTHR(hr = m_pRstEmployees.CreateInstance(__uuidof(ADODB::Recordset)));  
  
      m_pCnn->Open(strcnn, "", "", NULL);  
      m_pCatalog->PutActiveConnection(_variant_t((IDispatch *) m_pCnn));  
      m_pTable= m_pCatalog->Tables->GetItem("Employees");  
  
      // Create a new Field object and append it to the Fields  
      // collection of the Employees table.  
      m_pColumn->Name = "FaxPhone";  
      m_pColumn->Type = adVarWChar;  
      m_pColumn->DefinedSize = 24;  
      m_pColumn->Attributes = adColNullable;  
  
      m_pCatalog->Tables->GetItem("Employees")->Columns->Append(m_pColumn->Name, adVarWChar, 24);  
      // Append("FaxPhone", adVarWChar, 24);  
  
      // Open the Employees table for updating as a Recordset.  
      m_pRstEmployees->Open("Employees",   
         _variant_t((IDispatch *) m_pCnn),   
         ADODB::adOpenKeyset,   
         ADODB::adLockOptimistic,   
         ADODB::adCmdTable);  
  
      // Get user input.  
      printf("Enter fax number for : %s %s\n",   
         (LPSTR)(_bstr_t) m_pRstEmployees->Fields->GetItem("LastName")->Value,  
         (LPSTR) (_bstr_t) m_pRstEmployees->Fields->GetItem("FirstName")->Value);  
      printf("[? - unknown, X - has no fax] : \n");  
  
      // Skip user input routine, just supply a value.  
      char* strTemp = strtok_s("X", " \t", &token);  
      _variant_t vNull;  
      vNull.vt = VT_BSTR;  
      vNull.bstrVal = NULL;  
  
      if (strTemp != NULL) {  
         if (strcmp(strTemp, "?") == 0)  
            m_pRstEmployees->Fields->GetItem("FaxPhone")->PutValue(vNull);  
         else if( (strcmp(strTemp,"X") == 0) | (strcmp(strTemp,"x") == 0) )  
            m_pRstEmployees->Fields->GetItem("FaxPhone")->PutValue("");  
         else  
            m_pRstEmployees->Fields->GetItem("FaxPhone")->PutValue(strTemp);  
  
         m_pRstEmployees->Update();  
  
         // Open IADORecordBinding interface pointer for binding Recordset to a class  
         TESTHR(hr = m_pRstEmployees->QueryInterface(__uuidof(IADORecordBinding),(LPVOID*)&picRs));  
  
         // Bind the Recordset to a C++ class here  
         TESTHR(hr = picRs->BindToRecordset(&emprs));  
  
         // Print report.  
         printf("\nName - Fax number\n");  
         printf("%s %s ", emprs.lemp_LastNameStatus == adFldOK ?   
            emprs.m_szemp_LastName : "<NULL>",  
            emprs.lemp_FirstNameStatus == adFldOK ?   
            emprs.m_szemp_FirstName : "<NULL>");  
  
         if (emprs.lemp_FaxphoneStatus == adFldNull)  
            printf("- [Unknown]\n");  
         else if (strcmp((LPSTR)emprs.m_szemp_Faxphone, "") == 0)  
            printf("- [Has no fax]\n");  
         else  
            printf("- %s\n", emprs.m_szemp_Faxphone);  
      }  
  
      // Delete new field because this is a demonstration.    
      // m_pTable->Columns->Delete(m_pColumn->Name);  
   }  
   catch(_com_error &e) {  
      // Notify the user of errors if any.  
      _bstr_t bstrSource(e.Source());  
      _bstr_t bstrDescription(e.Description());  
  
      printf("\n\tSource :  %s \n\tdescription : %s \n ", (LPCSTR)bstrSource, (LPCSTR)bstrDescription);  
   }  
   catch(...) {  
      cout << "Error occurred in AttributesX...." << endl;  
   }  
  
   if (m_pRstEmployees)  
      if (m_pRstEmployees->State == 1)  
         m_pRstEmployees->Close();  
  
   // Delete new field because this is a demonstration.  
   if (m_pTable != NULL)  
      m_pTable->Columns->Delete(m_pColumn->Name);  
  
   if (m_pCnn)  
      if (m_pCnn->State == 1)  
         m_pCnn->Close();  
  
   // Release the IADORecordset Interface here  
   if (picRs)  
      picRs->Release();  
  
   ::CoUninitialize();  
}  
```  
  
## See Also  
 [Attributes Property (ADOX)](./attributes-property-adox.md)   
 [Column Object (ADOX)](./column-object-adox.md)