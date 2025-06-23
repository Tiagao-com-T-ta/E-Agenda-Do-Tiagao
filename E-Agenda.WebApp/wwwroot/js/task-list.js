function toggleDropdown(button) {
    const menu = button.nextElementSibling;
    const isVisible = menu.style.display === "block";

    document.querySelectorAll(".dropdown-menu").forEach(m => m.style.display = "none");
    menu.style.display = isVisible ? "none" : "block";

    if (!isVisible) {
        document.addEventListener("click", function handler(e) {
            if (!button.parentElement.contains(e.target)) {
                menu.style.display = "none";
                document.removeEventListener("click", handler);
            }
        });
    }
}

const searchInput = document.getElementById("searchInput");
const priorityFilter = document.getElementById("priorityFilter");
const statusFilter = document.getElementById("statusFilter");
const completionFilter = document.getElementById("completionFilter");
const creationDateFilter = document.getElementById("creationDateFilter");
const dateFromInput = document.getElementById("dateFrom");
const dateToInput = document.getElementById("dateTo");
const taskRows = document.querySelectorAll(".table-row");

const priorityMap = {
    low: "baixa",
    medium: "media",
    high: "alta"
};

function normalizarTexto(texto) {
    return texto.normalize("NFD").replace(/[\u0300-\u036f]/g, "").toLowerCase();
}

function filtrarTarefas() {
    const textoFiltro = normalizarTexto(searchInput.value.trim());
    const prioridadeSelecionada = priorityFilter.value;
    const statusSelecionado = statusFilter.value;
    const completionSelecionado = completionFilter.value;
    const creationDateSelecionado = creationDateFilter.value;
    const dateFromValue = dateFromInput.value; 
    const dateToValue = dateToInput.value;   

    taskRows.forEach(row => {
        const title = normalizarTexto(row.children[0].textContent);
        const prioridadeTextoTabela = normalizarTexto(row.children[1].textContent);
        const statusTexto = normalizarTexto(row.children[2].textContent);
        const completionTexto = row.children[3].textContent.replace("%", "").trim();
        const creationDateTexto = row.children[4].textContent.trim();

        const filtraTexto = title.includes(textoFiltro) ||
            prioridadeTextoTabela.includes(textoFiltro) ||
            statusTexto.includes(textoFiltro);

        const prioridadeIngles = prioridadeTextoTabela;
        const filtraPrioridade = prioridadeSelecionada === "todas" ||
            priorityMap[prioridadeIngles] === prioridadeSelecionada;

        const filtraStatus = statusSelecionado === "todos" ||
            (statusSelecionado === "concluido" && statusTexto.includes("concluido")) ||
            (statusSelecionado === "pendente" && statusTexto.includes("pendente"));

        const completionNum = parseInt(completionTexto, 10);
        let filtraCompletion = false;
        switch (completionSelecionado) {
            case "todos":
                filtraCompletion = true;
                break;
            case "0-25":
                filtraCompletion = completionNum >= 0 && completionNum <= 25;
                break;
            case "26-50":
                filtraCompletion = completionNum >= 26 && completionNum <= 50;
                break;
            case "51-75":
                filtraCompletion = completionNum >= 51 && completionNum <= 75;
                break;
            case "76-100":
                filtraCompletion = completionNum >= 76 && completionNum <= 100;
                break;
        }

        const hoje = new Date();
        const dataCriacao = new Date(creationDateTexto.split("/").reverse().join("-"));


        let filtraData = false;
        switch (creationDateSelecionado) {
            case "todos":
                filtraData = true;
                break;
            case "ultimos7dias":
                filtraData = (hoje - dataCriacao) / (1000 * 60 * 60 * 24) <= 7;
                break;
            case "ultimos30dias":
                filtraData = (hoje - dataCriacao) / (1000 * 60 * 60 * 24) <= 30;
                break;
            case "maisAntigo":
                filtraData = (hoje - dataCriacao) / (1000 * 60 * 60 * 24) > 30;
                break;
        }


        let filtraDataIntervalo = true; 
        if (dateFromValue) {
            const dateFrom = new Date(dateFromValue);
            if (dataCriacao < dateFrom) filtraDataIntervalo = false;
        }
        if (dateToValue) {
            const dateTo = new Date(dateToValue);
            dateTo.setHours(23, 59, 59, 999); 
            if (dataCriacao > dateTo) filtraDataIntervalo = false;
        }


        const filtroDataFinal = filtraData && filtraDataIntervalo;

        if (filtraTexto && filtraPrioridade && filtraStatus && filtraCompletion && filtroDataFinal) {
            row.style.display = "";
        } else {
            row.style.display = "none";
        }
    });
}

searchInput.addEventListener("input", filtrarTarefas);
priorityFilter.addEventListener("change", filtrarTarefas);
statusFilter.addEventListener("change", filtrarTarefas);
completionFilter.addEventListener("change", filtrarTarefas);
creationDateFilter.addEventListener("change", filtrarTarefas);
dateFromInput.addEventListener("change", filtrarTarefas);
dateToInput.addEventListener("change", filtrarTarefas);

filtrarTarefas();
