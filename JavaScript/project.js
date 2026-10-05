const projects = [
    {
        id: 1,
        title: "MyLibrary",
        category: "mobile",
        image: "images/boek.jpg",
        alt: "Screenshot van MyLibrary app",
        description: "MyLibrary is een persoonlijke boeken-app waarmee lezers via hun camera ISBN-barcodes scannen om hun boekenverzameling, leesstatussen en persoonlijk recensies moeiteloos te beheren en bij te houden.",
        technologies: [
            "Typescript & React Native (Expo)",
            "Supabase (PostgreSQL & OAuth",
            "Zustand (met AsyncStorage"
        ],
        githubUrl: "https://github.com/ADmrkynk/WPFW-Opdracht1"
    },
    {
        id: 2,
        title: "Interactive Analytics Dashboard",
        category: "frontend",
        image: "images/dashboard.jpg",
        alt: "Screenshot van Analytics Dashboard",
        description: "Voor dit project heb ik een interactief web-dashboard ontworpen en gebouwd waarmee kernstatistieken van een platform overzichtelijk worden gevisualiseerd. De focus lag op het helder structureren van complexe data en een moderne layout.",
        technologies: [
            "Semantische HTML5",
            "CSS3",
            "Git & GitHub"
        ],
        githubUrl: "https://github.com/ADmrkynk/WPFW-Opdracht1"
    },
    {
        id: 3,
        title: "Facto",
        category: "desktop",
        image: "images/facto.jpg",
        alt: "Screenshot van Facto desktop app",
        description: "Facto is een desktopapplicatie waarmee je eenvoudig klanten kunt beheren, gewerkte uren en tarieven kunt registreren en automatisch professionele PDF-facturen kunt genereren.",
        technologies: [
            "TypeScript met React (v19) & Tailwind CSS",
            "Tauri v2 aangedreven door Rust",
            "jsPDF voor exporteren van facturen"
        ],
        githubUrl: "https://github.com/ADmrkynk/WPFW-Opdracht1"
    }
];

function createProjectCard(project) {
    const article = document.createElement("article");

    const h2 = document.createElement("h2");
    h2.textContent = project.title;

    const img = document.createElement("img");
    img.src = project.image;
    img.alt = project.alt;
    img.width = 250;

    const pDesc = document.createElement("p");
    pDesc.textContent = project.description;

    const pTechHeader = document.createElement("p");
    const strong = document.createElement("strong");
    strong.textContent = "Gebruikte technologieën:";
    pTechHeader.appendChild(strong);

    const ul = document.createElement("ul");
    project.technologies.forEach(tech => {
        const li = document.createElement("li");
        li.textContent = tech;
        ul.appendChild(li);
    });

    const pLink = document.createElement("p");
    const link = document.createElement("a");
    link.href = project.githubUrl;
    link.target = "_blank";
    link.textContent = "Bekijk de code op Github \u2192";
    pLink.appendChild(link);

    article.appendChild(h2);
    article.appendChild(img);
    article.appendChild(pDesc);
    article.appendChild(pTechHeader);
    article.appendChild(ul);
    article.appendChild(pLink);

    return article;
}

function renderProjects(projectList) {
    const container = document.getElementById("projects-container");
    if (!container) return;

    container.innerHTML = "";

    projectList.forEach(project => {
        const card = createProjectCard(project);
        container.appendChild(card);
    });
}

function filterProjects(category) {
    if (category === "all") {
        renderProjects(projects);
    } else {
        const filtered = projects.filter(project => project.category === category);
        renderProjects(filtered)
    }
}

function setupFilterButtons() {
    const buttons = document.querySelectorAll(".filter-btn");

    buttons.forEach(button => {
        button.addEventListener("click", () => {
            buttons.forEach(btn => btn.classList.remove("active"));
            button.classList.add("active");

            const selectedCategory = button.getAttribute("data-category");
            filterProjects(selectedCategory);
        });
    });
}

document.addEventListener("DOMContentLoaded", () => {
    renderProjects(projects);
        setupFilterButtons();
});
