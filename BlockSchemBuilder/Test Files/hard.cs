int Main()
{
	string a = Console.ReadLine();
	if(a.Length > 10)
	{
		for (int i = 0; i < 10; i++)
		{
			result += a[i] + " ";
		}
	}
	else
	{
		while(a > "abc")
		{
			a.Insert(r.Next(0, 10), " ");
		}
	}
	Console.Write(c);
		
}