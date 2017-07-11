CallableStatement cstmt = con.prepareCall("{call employee_jobid(?)}");
String lname = "Brown";
cstmt.setString(1, lname);
Resultset rs = cstmt.executeQuery();
rs.close();
cstmt.close();