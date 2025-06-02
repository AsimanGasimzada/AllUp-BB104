function initializeRichTextEditors(className = 'rich-editor') {
    const editors = document.querySelectorAll(`.${className}`);

    editors.forEach(editor => {
        const wrapper = document.createElement('div');
        wrapper.classList.add('rte-wrapper');

        const toolbar = document.createElement('div');
        toolbar.classList.add('rte-toolbar');
        toolbar.innerHTML = `
            <button type="button" data-cmd="bold"><b>B</b></button>
            <button type="button" data-cmd="italic"><i>I</i></button>
            <button type="button" data-cmd="underline"><u>U</u></button>
            <button type="button" data-cmd="insertUnorderedList">• List</button>

            <select class="font-size-selector">
                <option value="">Font Size</option>
                <option value="1">Very Small</option>
                <option value="2">Small</option>
                <option value="3">Normal</option>
                <option value="4">Medium</option>
                <option value="5">Large</option>
                <option value="6">Very Large</option>
                <option value="7">Huge</option>
            </select>

            <select class="font-family-selector">
                <option value="">Font Family</option>
                <option value="Arial">Arial</option>
                <option value="Georgia">Georgia</option>
                <option value="Times New Roman">Times New Roman</option>
                <option value="Courier New">Courier New</option>
                <option value="Tahoma">Tahoma</option>
            </select>

            <input type="color" class="color-picker" title="Text Color" data-cmd="foreColor">
            <input type="color" class="color-picker" title="Background Color" data-cmd="backColor">
            <button type="button" class="removeBgBtn" data-cmd="removeBackColor">🚫 Clear</button>

            <button type="button" data-cmd="createLink">🔗 Link</button>
        `;

        const editable = document.createElement('div');
        editable.classList.add('rte-editor');
        editable.contentEditable = true;
        editable.innerHTML = editor.value || '<p>Buraya yazın...</p>';

        editor.style.display = 'none';

        wrapper.appendChild(toolbar);
        wrapper.appendChild(editable);
        editor.parentNode.insertBefore(wrapper, editor);
        wrapper.appendChild(editor);

        editable.addEventListener('input', () => {
            editor.value = editable.innerHTML;
        });

        // Event delegation for toolbar
        toolbar.addEventListener('click', e => {
            const btn = e.target.closest('button');
            if (!btn) return;
            e.preventDefault();

            const cmd = btn.dataset.cmd;


            if (cmd === 'removeBackColor') {
                document.execCommand('removeFormat', false, null);
                return;
            }

            if (cmd === 'createLink') {
                const val = prompt('Enter URL:');
                if (val) document.execCommand(cmd, false, val);
            } else {
                document.execCommand(cmd, false, null);
            }
        });

        // Font size & family dropdowns
        toolbar.querySelector('.font-size-selector')?.addEventListener('change', e => {
            document.execCommand('fontSize', false, e.target.value);
        });

        toolbar.querySelector('.font-family-selector')?.addEventListener('change', e => {
            document.execCommand('fontName', false, e.target.value);
        });

        toolbar.querySelector('.removeBgBtn')?.addEventListener('click', e => {
            document.execCommand('removeFormat', false, null);
        });



        // Color pickers
        const colorPickers = toolbar.querySelectorAll('.color-picker');
        colorPickers.forEach(input => {
            input.addEventListener('input', e => {
                const cmd = input.dataset.cmd;
                const val = input.value;
                document.execCommand(cmd, false, val);
            });
        });
    });
}


initializeRichTextEditors();