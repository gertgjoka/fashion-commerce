// blue style preload emails
function bluePreloadImage(path)
{
    //list of image that will be preloaded\    
    var preImageLinks = new Array("CloseDown.gif","CloseOut.gif","MinimizeOverDown.gif","MaximizeOut.gif");
    
    //preload action
    for(var i=0;i<preImageLinks.length;i++)
    {
        var img = new Image();
        img.src = path+"/"+preImageLinks[i];
        img.style.display = "none";
        document.body.insertBefore(img,document.body.firstChild);
    }
}
