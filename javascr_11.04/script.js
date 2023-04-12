let uto1 = document.querySelector("#uto1");
let uto2 = document.querySelector("#uto2");
let labda = document.querySelector(".labda");

const fullPalya=800;
const gamePalya=700;
const palyaMagassag=600;
const margo=(fullPalya-gamePalya)/2;
const utoMeret=100;
const utoSzelesseg=10;
const utoHossz=100;
const labdaMeret=10;

let uto1Y = palyaMagassag/2;
let uto2Y = palyaMagassag/2;
let labdaX = 0;
let labdaY = 0;
let labdaSebX = 0;
let labdaSebY = 0;
let score1 = 0;
let score2 = 0;

let uto1LeGomb = false;
let uto1FelGomb = false;
let uto2LeGomb = false;
let uto2FelGomb = false;

uto1.style.left = margo-utoSzelesseg+"px";
uto2.style.left = margo+gamePalya+"px";


//gomblenyomás
document.addEventListener("keydown", function(event)
{
    console.log(event);
    if(event.key == "s")
    {
        uto1LeGomb = true;
    }
    if(event.key == "ArrowDown")
    {
        uto2LeGomb = true;
    }
    if(event.key == "w")
    {
        uto1FelGomb = true;
    }
    if(event.key == "ArrowUp")
    {
        uto2FelGomb = true;
    }
});

document.addEventListener("keyup", function(event)
{
    console.log(event);
    if(event.key == "s")
    {
        uto1LeGomb = false;
    }
    if(event.key == "ArrowDown")
    {
        uto2LeGomb = false;
    }
    if(event.key == "w")
    {
        uto1FelGomb = false;
    }
    if(event.key == "ArrowUp")
    {
        uto2FelGomb = false;
    }
});

function loop()
{
    if(uto1LeGomb)
    {
        uto1Y += 5;
    }
    if(uto1FelGomb)
    {
        uto1Y -= 5;
    }
    if(uto2LeGomb)
    {
        uto2Y += 5;
    }
    if(uto2FelGomb)
    {
        uto2Y -= 5;
    }

    labdaX += labdaSebX;
    labdaY += labdaSebY;

    //falpattanás

    if(labdaY<=labdaMeret/2 || labdaY>=palyaMagassag){
        labdaSebY=-labdaSebY
    }

    //bal-jobb kimegy

    if(labdaX<=-margo)
    {
        startLabda();
        score1++;
        document.querySelector(".score1").innerHTML = "Score: " + score1;
    }
    else if(labdaX>=labdaMeret+gamePalya+margo)
    {
        startLabda();
        score2++;
        document.querySelector(".score2").innerHTML = "Score: " + score2;
    }

    //ütőpattanás

    if(labdaX<0 && (labdaX> -labdaMeret/2 || labdaX >= labdaSebX - utoSzelesseg/2) && Math.abs(labdaY-uto1Y) <= utoHossz/2+labdaMeret/2){
        labdaX=0;
        labdaSebX=-labdaSebX*1.1;
        labdaSebY=labdaSebY*1.1;
    }

    if(labdaX>gamePalya && (labdaX< gamePalya+labdaMeret/2 || labdaX - gamePalya <= labdaSebX + utoSzelesseg/2) && Math.abs(labdaY-uto2Y) <= utoHossz/2+labdaMeret/2){
        labdaX=gamePalya;
        labdaSebX=-labdaSebX*1.1;
        labdaSebY=labdaSebY*1.1;
    }

    uto1.style.top = uto1Y- utoHossz/2 + "px";
    uto2.style.top = uto2Y- utoHossz/2 + "px";

    labda.style.left = labdaX - labdaMeret/2 + margo + "px";
    labda.style.top = labdaY - labdaMeret/2 + "px";


    window.requestAnimationFrame(loop);
}

function startLabda()
{
    labdaX = gamePalya/2;
    labdaY = palyaMagassag/2;

    labdaSebX = (Math.random()*3+2) * (Math.round(Math.random())*2-1);
    labdaSebY = (Math.random()*3+2) * (Math.round(Math.random())*2-1);
}

startLabda();
loop();