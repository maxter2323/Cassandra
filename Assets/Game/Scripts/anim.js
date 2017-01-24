#pragma strict

function Start () {
	for (var state : AnimationState in GetComponent.<Animation>()) {
     state.speed = 0.5;
 } 
}

function Update () {

}