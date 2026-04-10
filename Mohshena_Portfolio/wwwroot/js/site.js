// Theme toggle
const toggle = document.getElementById("theme-toggle");
toggle.addEventListener("click", () => {
    document.body.classList.toggle("light-mode");
});

// Mobile menu toggle
function toggleMenu() {
    document.getElementById("navLinks").classList.toggle("active");
}

// Typewriter effect
const role = ".Net Developer";
let charIndex = 0;
const dynamicText = document.querySelector('.dynamic-text');

function animateText() {
    dynamicText.textContent = role.substring(0, charIndex);
    charIndex++;
    if (charIndex <= role.length) {
        setTimeout(animateText, 150); // typing speed
    } else {
        // Wait then restart
        setTimeout(() => { charIndex = 0; animateText(); }, 2000);
    }
}
animateText();

// Fade-up animations for sections
const sections = document.querySelectorAll('.about-section, .skills-section, .projects-section');

const observer = new IntersectionObserver(entries => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add('show');
            observer.unobserve(entry.target);
        }
    });
}, { threshold: 0.3 });

sections.forEach(section => observer.observe(section));