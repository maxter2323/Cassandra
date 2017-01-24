using UnityEngine;
using System.Collections;

public interface IRequirement 
{
	bool CheckRequirement();
	string DescriptionString();
}
