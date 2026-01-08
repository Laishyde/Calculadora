document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('calcForm');
    const labels = document.querySelectorAll('.operation-label');

    // Bloquear outros radios ao clicar em qualquer label
    labels.forEach(label => {
        label.addEventListener('click', () => {
            labels.forEach(l => {
                if (l !== label) {
                    const radio = document.getElementById(l.getAttribute('for'));
                    if (radio) radio.disabled = true;

                    // Visual de bloqueado
                    l.classList.add('opacity-50', 'cursor-not-allowed', 'relative');

                    if (!l.querySelector('.lock-icon')) {
                        const span = document.createElement('span');
                        span.textContent = '🚫';
                        span.classList.add('lock-icon', 'absolute', 'top-2', 'right-2', 'text-red-600', 'text-xl');
                        l.appendChild(span);
                    }
                }
            });
        });
    });

    // Modal e reset
    const modal = document.getElementById('feedbackModal');
    if (modal) {
        modal.classList.remove('opacity-0', 'pointer-events-none');
        modal.classList.add('scale-100');

        setTimeout(() => {
            modal.classList.add('opacity-0', 'pointer-events-none');

            // Resetar campos e desbloquear radios
            const valorA = document.querySelector('input[name="ValorA"]');
            const valorB = document.querySelector('input[name="ValorB"]');
            const radios = document.querySelectorAll('input[name="Operacao"]');

            if (valorA) valorA.value = '';
            if (valorB) valorB.value = '';
            radios.forEach(r => r.checked = false);
            radios.forEach(r => r.disabled = false);

            // Remover ícones de bloqueio
            document.querySelectorAll('.lock-icon').forEach(e => e.remove());
            labels.forEach(l => l.classList.remove('opacity-50', 'cursor-not-allowed'));
        }, 3000);
    }
});