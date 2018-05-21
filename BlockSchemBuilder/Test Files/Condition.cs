int Main()
{
	int a = int.Parse(Console.ReadLine());
	int b = int.Parse(Console.ReadLine());
	int c;
	if(a > b)
	{
		c = a + b;
	}
	else
	{
		c = b - a;
	}
	Console.Write(c);
		
}