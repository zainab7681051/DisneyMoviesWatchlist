const app = document.getElementById('app')
const img = document.querySelectorAll('img');
const loading_screen = document.querySelector('.loading-screen-wrapper')
let img_count = 0;
for (let i = 0; i < img.length; i++) {
    console.log('loading image', img_count);
    img[i].onload = () => {
        img_count++;
        console.log('loaded', img_count);
        if (img_count >= 2) {
            loading_screen.style.display = "none";
            app.removeAttribute('style');
        }
    }
}
