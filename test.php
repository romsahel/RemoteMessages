<!DOCTYPE html>
<html>
<head>
<meta http-equiv="content-type" content="text/html; charset=iso-8859-1" />
<title>Submit example</title>
</head>
<body>
<?php
		// Open the file in write mode
		$handle = fopen("./writetest.txt","w+"); 
		
		// Write to that handle the username submitted in the form and the date
		fwrite($handle, "coucou");
			
		// Close the file
		fclose($handle);
?>
<!-- Notice how the form is submitting to itself -->
<form action="test.php" method="POST">
		<p>
			NAME: <br>
			<input type="text" name="usersname" maxlength="50" value="">
		</p>			
		<input type="submit" value="Write" name="submitwrite"/>	
</form>
</body>

</html>

