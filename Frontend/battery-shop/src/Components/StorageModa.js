import React from 'react';

import {Modal} from "antd";

export default function StorageModal({ visible, handleClose }) {
    return <Modal
        visible={visible}
        onCancel={handleClose}
        footer={false}
        closable={false}
    >

    </Modal>
}
