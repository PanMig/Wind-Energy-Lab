var BlinkSpeed : float = 1.0;
var LightOnIntensity : float = 1.0;
var LightOffIntensity : float = 0.0;
private var SecondsPassed : float = 0.0;

function Start(){GetComponent.<Light>().intensity = LightOnIntensity;}
function Update() 
{
	SecondsPassed += Time.deltaTime;
	if(GetComponent.<Light>().intensity == LightOnIntensity && SecondsPassed >= BlinkSpeed)
	{
		GetComponent.<Light>().intensity = LightOffIntensity;
		SecondsPassed = 0.0;
	}
	
	else if(GetComponent.<Light>().intensity == LightOffIntensity && SecondsPassed >= BlinkSpeed)
	{
		GetComponent.<Light>().intensity = LightOnIntensity;
		SecondsPassed = 0.0;
	}
}

